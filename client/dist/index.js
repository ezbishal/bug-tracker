import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
var rootElement = document.querySelector('#root');
if (rootElement == null) {
    throw new Error('Root element not found');
}
var root = ReactDOM.createRoot(rootElement);
root.render(React.createElement(React.StrictMode, null,
    React.createElement(App, null)));
