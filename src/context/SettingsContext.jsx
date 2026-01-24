import React, { createContext, useState, useContext } from 'react';

const SettingsContext = createContext();

export const useSettings = () => useContext(SettingsContext);

export const SettingsProvider = ({ children }) => {
    const [settings, setSettings] = useState({
        // General
        theme: 'Dark',
        language: 'English',
        // Audio
        defaultAudioFormat: 'MP3',
        // Video
        enableGPU: true,
        // Subtitles
        subtitleEncoding: 'Default (Windows-1252)',
        subtitlePosition: 80,
        subtitleScale: 130,
        subtitleColor: '#FFFFFF', // Default white
        useCustomSubtitleColor: true,
        subtitleTransparency: 0,
        subtitleFont: 'Tahoma',
        subtitleBold: true,
        subtitleItalic: false,
        subtitleUnderline: false,
        subtitleStrikethrough: false,
        subtitleShadow: true,
        subtitleShadowBlur: 1,
        subtitleBorder: true,
        subtitleBorderWidth: 0,
    });

    const updateSetting = (key, value) => {
        setSettings(prev => ({ ...prev, [key]: value }));
    };

    return (
        <SettingsContext.Provider value={{ settings, updateSetting }}>
            {children}
        </SettingsContext.Provider>
    );
};
