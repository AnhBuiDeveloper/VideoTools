import React, { useState } from 'react';
import { Layout } from './components/Layout';
import { GeneralSettings } from './pages/GeneralSettings';
import { VideoConvert } from './pages/VideoConvert';
import { SettingsProvider } from './context/SettingsContext';

function App() {
  const [activeTab, setActiveTab] = useState('settings');

  const renderContent = () => {
    switch (activeTab) {
      case 'settings':
        return <GeneralSettings />;
      case 'convert':
        return <VideoConvert />;
      default:
        return <GeneralSettings />;
    }
  };

  return (
    <SettingsProvider>
      <Layout activeTab={activeTab} onTabChange={setActiveTab}>
        {renderContent()}
      </Layout>
    </SettingsProvider>
  );
}

export default App;
