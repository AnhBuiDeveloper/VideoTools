import React from 'react';
import { TabNavigation } from './Tabs';

export const Layout = ({ activeTab, onTabChange, children }) => {
    return (
        <div className="flex full-h">
            <TabNavigation activeTab={activeTab} onTabChange={onTabChange} />
            <main className="flex-1 overflow-auto bg-dark">
                {children}
            </main>
        </div>
    );
};
