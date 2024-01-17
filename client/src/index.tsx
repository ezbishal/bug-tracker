import React from 'react';
import ReactDOM, {Root} from 'react-dom/client';
import './index.css';
import App from './App';

const rootElement : HTMLElement | null = document.querySelector('#root');
if(rootElement == null){
  throw new Error('Root element not found');
}

const root : Root = ReactDOM.createRoot(rootElement as HTMLElement);
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
