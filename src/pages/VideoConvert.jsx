import React, { useState, useRef, useEffect } from 'react';
import { Settings, Loader2 } from 'lucide-react';
import './VideoConvert.css';
import { useSettings } from '../context/SettingsContext';
import { FFmpeg } from '@ffmpeg/ffmpeg';
import { toBlobURL } from '@ffmpeg/util';

export const VideoConvert = () => {
    const [format, setFormat] = useState('mkv');
    const [videoFile, setVideoFile] = useState('');
    const [subtitleFile, setSubtitleFile] = useState('');
    const [status, setStatus] = useState('idle'); // idle, converting, success, error
    const [progress, setProgress] = useState(0);
    const [message, setMessage] = useState('');
    const [logs, setLogs] = useState([]);

    const { settings } = useSettings();
    const ffmpegRef = useRef(new FFmpeg());
    const videoInputRef = useRef(null);
    const subtitleInputRef = useRef(null);
    const [videoFileObj, setVideoFileObj] = useState(null);
    const [subtitleFileObj, setSubtitleFileObj] = useState(null);

    // Helpers
    const log = (msg) => {
        setLogs(prev => [...prev, msg]);
        console.log(msg);
    };

    const readFromBlobOrFile = (blob) => {
        return new Promise((resolve, reject) => {
            const fileReader = new FileReader();
            fileReader.onload = () => resolve(new Uint8Array(fileReader.result));
            fileReader.onerror = (e) => reject(Error(`File read failed: ${fileReader.error}`));
            fileReader.readAsArrayBuffer(blob);
        });
    };

    const handleFileSelect = (ref) => ref.current.click();

    const onVideoSelected = (e) => {
        if (e.target.files && e.target.files[0]) {
            const file = e.target.files[0];
            setVideoFile(file.name);
            setVideoFileObj(file);
        }
    };

    const onSubtitleSelected = (e) => {
        if (e.target.files && e.target.files[0]) {
            setSubtitleFile(e.target.files[0].name);
            setSubtitleFileObj(e.target.files[0]);
        }
    };

    const load = async () => {
        const ffmpeg = ffmpegRef.current;

        // Setup event listeners
        ffmpeg.on('log', ({ message }) => {
            log(`[ffmpeg] ${message}`);
        });

        ffmpeg.on('progress', ({ progress }) => {
            setProgress(Math.round(progress * 100));
        });

        if (ffmpeg.loaded) return;

        setMessage('Loading FFmpeg (0.12 ST)...');
        log('Loading Config: Single-Threaded Mode');

        try {
            // CDN Loading - Bypass local build artifacts issues
            // Using 0.12.10 Single Threaded Core
            const CDN_BASE = 'https://unpkg.com/@ffmpeg/core@0.12.10/dist/esm';
            log(`Loading Core from CDN: ${CDN_BASE}`);

            await ffmpeg.load({
                coreURL: `${CDN_BASE}/ffmpeg-core.js`,
                wasmURL: `${CDN_BASE}/ffmpeg-core.wasm`,
            });

            setMessage('FFmpeg Loaded!');
            log('Load Success!');
        } catch (error) {
            console.error(error);
            const errStr = typeof error === 'object' ? JSON.stringify(error, Object.getOwnPropertyNames(error)) : String(error);
            setMessage(`Load Error: ${error.message || 'Check Logs'}`);
            log(`CRITICAL LOAD ERROR: ${errStr}`);
            throw error;
        }
    };

    const handleConvert = async () => {
        if (!videoFile) {
            alert('Please select a source video file first.');
            return;
        }

        setStatus('converting');
        setProgress(0);
        setLogs([]);
        setMessage('Starting conversion...');

        const ffmpeg = ffmpegRef.current;

        try {
            await load();

            const mountDir = '/mnt';
            try { await ffmpeg.deleteDir(mountDir); } catch (e) { }
            try { await ffmpeg.createDir(mountDir); } catch (e) { }

            // MOUNT STRATEGY (Hybrid)
            setMessage('Mounting large file...');
            const safeName = 'input.mkv';
            const safeFile = new File([videoFileObj], safeName, { type: videoFileObj.type });

            try {
                if (ffmpeg.mount) {
                    await ffmpeg.mount('WORKERFS', { files: [safeFile] }, mountDir);
                } else {
                    // Fallback to internal module access
                    // @ts-ignore
                    const FS = ffmpeg.module.FS;
                    const WORKERFS = ffmpeg.module.WORKERFS;
                    if (!FS || !WORKERFS) throw new Error("FS/WORKERFS not found in module");
                    FS.mount(WORKERFS, { files: [safeFile] }, mountDir);
                }
                log('Mount Success (WORKERFS)');
            } catch (e) {
                console.error(e);
                throw new Error(`Mount Failed: ${e.message}. Large files require mounting.`);
            }

            const inputPath = `${mountDir}/${safeName}`;

            let command = ['-i', inputPath];
            if (subtitleFileObj) {
                const subData = await readFromBlobOrFile(subtitleFileObj);
                await ffmpeg.writeFile('sub.srt', subData);
                command.push('-vf', 'subtitles=sub.srt');
            }

            // Optimize for speed in WebAssembly
            command.push('-preset', 'ultrafast');

            command.push(`output.${format}`);

            setMessage('Exec FFmpeg...');
            log(`CMD: ${command.join(' ')}`);

            await ffmpeg.exec(command);

            setMessage('Reading output...');
            const data = await ffmpeg.readFile(`output.${format}`);

            setMessage('Creating download...');
            const url = URL.createObjectURL(new Blob([data.buffer], { type: 'video/mp4' }));
            const a = document.createElement('a');
            a.href = url;
            a.download = `converted.${format}`;
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
            URL.revokeObjectURL(url);

            setStatus('success');
            setMessage('Complete!');

            // Cleanup
            try {
                if (ffmpeg.unmount) {
                    await ffmpeg.unmount(mountDir);
                } else {
                    ffmpeg.module.FS.unmount(mountDir);
                }
                await ffmpeg.deleteFile(`output.${format}`);
                if (subtitleFileObj) await ffmpeg.deleteFile('sub.srt');
            } catch (e) { }

        } catch (error) {
            console.error(error);
            setMessage(`Error: ${error.message}`);
            log(error.message);
            setStatus('error');
        }
    };

    return (
        <div className="convert-page">
            <div className="convert-header">
                <h2>Video Convert (v0.12 - Large File Support)</h2>
            </div>

            {/* Inputs Section */}
            <div className="convert-card">
                <span className="card-title">Source Files</span>
                <div className="input-row">
                    <div className="file-input-wrapper">
                        <label>Source Video</label>
                        <div className="custom-file-input">
                            <input
                                type="text"
                                className="file-path"
                                placeholder="Select video file..."
                                readOnly
                                value={videoFile}
                            />
                            <input
                                type="file"
                                hidden
                                ref={videoInputRef}
                                accept="video/*,.mkv"
                                onChange={onVideoSelected}
                            />
                            <button
                                className="btn-upload"
                                onClick={() => handleFileSelect(videoInputRef)}
                            >
                                Browse
                            </button>
                        </div>
                    </div>
                    <div className="file-input-wrapper">
                        <label>Embed Subtitle (Optional)</label>
                        <div className="custom-file-input">
                            <input
                                type="text"
                                className="file-path"
                                placeholder="Select .srt file..."
                                readOnly
                                value={subtitleFile}
                            />
                            <input
                                type="file"
                                hidden
                                ref={subtitleInputRef}
                                accept=".srt"
                                onChange={onSubtitleSelected}
                            />
                            <button
                                className="btn-upload"
                                onClick={() => handleFileSelect(subtitleInputRef)}
                            >
                                Browse
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            {/* Settings Section */}
            <div className="convert-card">
                <div className="flex justify-between items-center mb-4 border-b border-gray-700 pb-2">
                    <span className="text-accent font-semibold">Conversion Settings</span>
                    <div className="flex items-center gap-2">
                        <label className="text-sm text-gray-400">Output:</label>
                        <select
                            className="control-input"
                            value={format}
                            onChange={(e) => setFormat(e.target.value)}
                        >
                            <option value="mkv">MKV</option>
                            <option value="mp4">MP4</option>
                            <option value="avi">AVI</option>
                        </select>
                    </div>
                </div>

                {format === 'mkv' && (
                    <div className="settings-grid-compact">
                        {/* Basic Column */}
                        <div className="setting-column">
                            <span className="column-header">Basic</span>
                            <div className="control-row">
                                <label>Size</label>
                                <div className="flex items-center">
                                    <select className="control-input" defaultValue="Original">
                                        <option>Original</option>
                                        <option>1920x1080</option>
                                        <option>1280x720</option>
                                    </select>
                                    <Settings size={14} className="settings-icon" />
                                </div>
                            </div>
                            <div className="control-row">
                                <label>Quality</label>
                                <select className="control-input" defaultValue="High">
                                    <option value="High">High</option>
                                    <option value="Medium">Medium</option>
                                    <option value="Low">Low</option>
                                </select>
                            </div>
                        </div>

                        {/* Video Column */}
                        <div className="setting-column">
                            <span className="column-header">Video Options</span>
                            <div className="control-row">
                                <label>Codec</label>
                                <select className="control-input" defaultValue="x264">
                                    <option>x264</option>
                                    <option>h264</option>
                                </select>
                            </div>
                            <div className="control-row">
                                <label>Bitrate</label>
                                <div className="flex items-center">
                                    <select className="control-input" defaultValue="4000">
                                        <option>4000</option>
                                        <option>6000</option>
                                    </select>
                                    <Settings size={14} className="settings-icon" />
                                </div>
                            </div>
                            <div className="control-row">
                                <label>Frame Rate</label>
                                <div className="flex items-center">
                                    <select className="control-input" defaultValue="Auto">
                                        <option>Auto</option>
                                        <option>30</option>
                                        <option>60</option>
                                    </select>
                                    <Settings size={14} className="settings-icon" />
                                </div>
                            </div>
                            <div className="control-row">
                                <label>Aspect</label>
                                <select className="control-input" defaultValue="Auto">
                                    <option>Auto</option>
                                    <option>16:9</option>
                                </select>
                            </div>
                        </div>

                        {/* Audio Column */}
                        <div className="setting-column">
                            <span className="column-header">Audio Options</span>
                            <div className="control-row">
                                <label>Codec</label>
                                <select className="control-input" defaultValue="pcm">
                                    <option>pcm</option>
                                    <option>aac</option>
                                    <option>mp3</option>
                                </select>
                            </div>
                            <div className="control-row">
                                <label>Bitrate</label>
                                <div className="flex items-center">
                                    <select className="control-input" defaultValue="128">
                                        <option>128</option>
                                        <option>192</option>
                                    </select>
                                    <Settings size={14} className="settings-icon" />
                                </div>
                            </div>
                            <div className="control-row">
                                <label>Sample Rate</label>
                                <select className="control-input" defaultValue="44100">
                                    <option>44100</option>
                                    <option>48000</option>
                                </select>
                            </div>
                            <div className="control-row">
                                <label>Channels</label>
                                <select className="control-input" defaultValue="2">
                                    <option>2</option>
                                    <option>1</option>
                                </select>
                            </div>
                        </div>
                    </div>
                )}
            </div>

            <div className="action-area">
                <button
                    className="btn-convert"
                    onClick={handleConvert}
                    disabled={status === 'converting'}
                >
                    {status === 'converting' ? 'Converting...' : 'Convert Now'}
                </button>
            </div>

            {status !== 'idle' && (
                <div className="conversion-overlay">
                    <div className="conversion-modal">
                        <div className="flex justify-between items-center w-full mb-2">
                            <h3>
                                {status === 'converting' ? 'Converting Video...' :
                                    status === 'success' ? 'Conversion Success' : 'Conversion Failed'}
                            </h3>
                            {status !== 'converting' && (
                                <button
                                    className="text-gray-400 hover:text-white"
                                    onClick={() => setStatus('idle')}
                                >
                                    ✕
                                </button>
                            )}
                        </div>

                        {(status === 'converting' || progress > 0) && (
                            <div className="progress-bar">
                                <div className="progress-fill" style={{ width: `${progress}%` }}></div>
                            </div>
                        )}

                        <p>{progress}% Complete</p>
                        <div className="conversion-details">
                            <p className="status-message">{message}</p>

                            {/* Detailed Log Window */}
                            <div className="log-window">
                                {logs.map((log, i) => (
                                    <div key={i} className="log-line">{log}</div>
                                ))}
                            </div>

                            <p className="mt-2 text-xs text-gray-500">Using GPU: {settings.enableGPU ? 'Yes' : 'No'}</p>
                            {subtitleFile && <p className="text-xs text-gray-500">Embedding Subtitles: Yes</p>}
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};
