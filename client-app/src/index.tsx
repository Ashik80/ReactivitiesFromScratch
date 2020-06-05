import React from 'react';
import ReactDOM from 'react-dom';
import './app/layout/styles.css';
import App from './app/layout/App';
import * as serviceWorker from './serviceWorker';
import { BrowserRouter } from 'react-router-dom'
import ScrollToTop from './app/layout/ScrollToTop';

ReactDOM.render(
  <BrowserRouter>
    <ScrollToTop>
      <App />
    </ScrollToTop>
  </BrowserRouter>,
  document.getElementById('root')
);
serviceWorker.unregister();