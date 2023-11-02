import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './layout/App.tsx'
import './index.css'

// อีกชื่อคือ index.tsx

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
  
);