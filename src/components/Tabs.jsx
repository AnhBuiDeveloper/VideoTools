import React, { useState } from 'react';
import { Settings, Video } from 'lucide-react';
import './Tabs.css';

export const TabNavigation = ({ activeTab, onTabChange }) => {
    return (
        <div className="sidebar">
            <div className="logo-area">
                <h1>Video Tools</h1>
            </div>
            <nav className="nav-menu">
                <button
                    className={`nav-item ${activeTab === 'settings' ? 'active' : ''}`}
                    onClick={() => onTabChange('settings')}
                >
                    <Settings size={20} />
                    <span>General Settings</span>
                </button>
                <button
                    className={`nav-item ${activeTab === 'convert' ? 'active' : ''}`}
                    onClick={() => onTabChange('convert')}
                >
                    <Video size={20} />
                    <span>Video Convert</span>
                </button>
            </nav>
        </div>
    );
};
