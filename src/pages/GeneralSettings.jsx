import React, { useState } from 'react';
import './GeneralSettings.css';

import { useSettings } from '../context/SettingsContext';

export const GeneralSettings = () => {
    const [activeSubTab, setActiveSubTab] = useState('general');
    const { settings, updateSetting } = useSettings();

    const renderContent = () => {
        switch (activeSubTab) {
            case 'general':
                return (
                    <div className="settings-panel">
                        <h3>Application Settings</h3>
                        <div className="form-group">
                            <label>Theme</label>
                            <select
                                value={settings.theme}
                                onChange={(e) => updateSetting('theme', e.target.value)}
                            >
                                <option>Dark</option>
                                <option>Light</option>
                                <option>System</option>
                            </select>
                        </div>
                        <div className="form-group">
                            <label>Language</label>
                            <select>
                                <option>English</option>
                                <option>Spanish</option>
                                <option>French</option>
                            </select>
                        </div>
                    </div>
                );
            case 'audio':
                return (
                    <div className="settings-panel">
                        <h3>Audio Settings</h3>
                        <div className="form-group">
                            <label>Default Output Format</label>
                            <select>
                                <option>MP3</option>
                                <option>WAV</option>
                                <option>AAC</option>
                            </select>
                        </div>
                    </div>
                );
            case 'video':
                return (
                    <div className="settings-panel">
                        <h3>Video Settings</h3>

                        <div className="form-group">
                            <label>Hardware Acceleration</label>
                            <input
                                type="checkbox"
                                checked={settings.enableGPU}
                                onChange={(e) => updateSetting('enableGPU', e.target.checked)}
                            /> Enable GPU Acceleration
                        </div>
                    </div>
                );
            case 'subtitles':
                return (
                    <div className="settings-panel subtitles-panel">
                        {/* Encoding Section */}
                        <div className="settings-section">
                            <span className="section-label">Encoding</span>
                            <div className="form-group row">
                                <label>Default subtitle encoding:</label>
                                <select
                                    value={settings.subtitleEncoding}
                                    onChange={(e) => updateSetting('subtitleEncoding', e.target.value)}
                                >
                                    <option>Default (Windows-1252)</option>
                                    <option>UTF-8</option>
                                    <option>ASCII</option>
                                </select>
                            </div>
                        </div>

                        {/* Position Section */}
                        <div className="settings-section">
                            <span className="section-label">Position</span>
                            <div className="form-group">
                                <label>Default position of the subtitles on screen:</label>
                                <div className="slider-container">
                                    <span>Bottom</span>
                                    <input
                                        type="range"
                                        min="0"
                                        max="100"
                                        value={settings.subtitlePosition}
                                        onChange={(e) => updateSetting('subtitlePosition', parseInt(e.target.value))}
                                    />
                                    <span>Top</span>
                                    <span className="value-display">{settings.subtitlePosition}</span>
                                </div>
                            </div>
                        </div>

                        {/* Size Section */}
                        <div className="settings-section">
                            <span className="section-label">Size</span>
                            <div className="form-group row">
                                <label>Default scale:</label>
                                <div className="input-with-unit">
                                    <input
                                        type="number"
                                        value={settings.subtitleScale}
                                        onChange={(e) => updateSetting('subtitleScale', parseInt(e.target.value))}
                                    />
                                    <span>%</span>
                                </div>
                            </div>
                        </div>

                        {/* Color Section */}
                        <div className="settings-section">
                            <span className="section-label">Color</span>
                            <div className="form-group">
                                <div className="checkbox-group">
                                    <input
                                        type="checkbox"
                                        checked={settings.useCustomSubtitleColor}
                                        onChange={(e) => updateSetting('useCustomSubtitleColor', e.target.checked)}
                                    /> Using the custom subtitles color
                                </div>
                                <div className="color-controls">
                                    <button className="btn-secondary">Select subtitle color</button>
                                    <span className="color-preview-text" style={{ color: settings.subtitleColor }}>
                                        Subtitles will be displayed in this color
                                    </span>
                                    <div className="transparency-control">
                                        <label>Transparency:</label>
                                        <input
                                            type="range"
                                            min="0"
                                            max="100"
                                            value={settings.subtitleTransparency}
                                            onChange={(e) => updateSetting('subtitleTransparency', parseInt(e.target.value))}
                                        />
                                        <span>{settings.subtitleTransparency}%</span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        {/* Font Section */}
                        <div className="settings-section">
                            <span className="section-label">Font</span>
                            <div className="form-group">
                                <label>Select the font used for subtitles:</label>
                                <select
                                    value={settings.subtitleFont}
                                    onChange={(e) => updateSetting('subtitleFont', e.target.value)}
                                >
                                    <option>Tahoma</option>
                                    <option>Arial</option>
                                    <option>Helvetica</option>
                                    <option>Times New Roman</option>
                                </select>
                            </div>

                            <div className="font-styles">
                                <div className="checkbox-group">
                                    <input
                                        type="checkbox"
                                        checked={settings.subtitleBold}
                                        onChange={(e) => updateSetting('subtitleBold', e.target.checked)}
                                    /> Bold
                                </div>
                                <div className="checkbox-group">
                                    <input
                                        type="checkbox"
                                        checked={settings.subtitleItalic}
                                        onChange={(e) => updateSetting('subtitleItalic', e.target.checked)}
                                    /> Italic
                                </div>
                                <div className="checkbox-group">
                                    <input
                                        type="checkbox"
                                        checked={settings.subtitleUnderline}
                                        onChange={(e) => updateSetting('subtitleUnderline', e.target.checked)}
                                    /> Underline
                                </div>
                                <div className="checkbox-group">
                                    <input
                                        type="checkbox"
                                        checked={settings.subtitleStrikethrough}
                                        onChange={(e) => updateSetting('subtitleStrikethrough', e.target.checked)}
                                    /> Strikethrough
                                </div>
                            </div>

                            <div className="font-effects">
                                <div className="effect-group">
                                    <div className="checkbox-group">
                                        <input
                                            type="checkbox"
                                            checked={settings.subtitleShadow}
                                            onChange={(e) => updateSetting('subtitleShadow', e.target.checked)}
                                        /> Shadow effect
                                    </div>
                                    <label>Shadow blur width:</label>
                                    <input
                                        type="number"
                                        value={settings.subtitleShadowBlur}
                                        onChange={(e) => updateSetting('subtitleShadowBlur', parseInt(e.target.value))}
                                        className="small-input"
                                    />
                                </div>
                                <div className="effect-group">
                                    <div className="checkbox-group">
                                        <input
                                            type="checkbox"
                                            checked={settings.subtitleBorder}
                                            onChange={(e) => updateSetting('subtitleBorder', e.target.checked)}
                                        /> Adding borders
                                    </div>
                                    <label>Border width:</label>
                                    <input
                                        type="number"
                                        value={settings.subtitleBorderWidth}
                                        onChange={(e) => updateSetting('subtitleBorderWidth', parseInt(e.target.value))}
                                        className="small-input"
                                    />
                                    <button className="btn-small">Border color</button>
                                </div>
                            </div>
                        </div>
                    </div>
                );
            default:
                return <div>Select a setting category</div>;
        }
    };

    return (
        <div className="settings-page">
            <div className="settings-header">
                <h2>General Settings</h2>
                <div className="settings-tabs">
                    <button
                        className={`tab-btn ${activeSubTab === 'general' ? 'active' : ''}`}
                        onClick={() => setActiveSubTab('general')}
                    >
                        General
                    </button>
                    <button
                        className={`tab-btn ${activeSubTab === 'audio' ? 'active' : ''}`}
                        onClick={() => setActiveSubTab('audio')}
                    >
                        Audio
                    </button>
                    <button
                        className={`tab-btn ${activeSubTab === 'video' ? 'active' : ''}`}
                        onClick={() => setActiveSubTab('video')}
                    >
                        Video
                    </button>
                    <button
                        className={`tab-btn ${activeSubTab === 'subtitles' ? 'active' : ''}`}
                        onClick={() => setActiveSubTab('subtitles')}
                    >
                        Subtitles
                    </button>
                </div>
            </div>
            <div className="settings-content">
                {renderContent()}
            </div>
        </div>
    );
};
