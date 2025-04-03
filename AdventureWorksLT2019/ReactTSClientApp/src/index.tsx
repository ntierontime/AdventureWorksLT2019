import React, { Suspense } from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { Provider } from 'react-redux';
import store, { persistor } from './store/Store';
import "./i18n";
import { PersistGate } from 'reduxjs-toolkit-persist/integration/react'
import { GoogleOAuthProvider } from '@react-oauth/google';

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);
root.render(
    
    <Suspense fallback={<div>...</div>}>
        <GoogleOAuthProvider clientId="231032877404-lnl95rkjik29sdfg2bbq2pjm1a249eg8.apps.googleusercontent.com">
        <React.StrictMode>
            <Provider store={store}>
                <PersistGate loading={null} persistor={persistor}>
                    <App />
                </PersistGate>
            </Provider>
        </React.StrictMode>
        </GoogleOAuthProvider>
    </Suspense>

);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
