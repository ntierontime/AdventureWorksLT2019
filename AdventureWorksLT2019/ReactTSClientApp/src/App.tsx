import { createTheme, CssBaseline, PaletteMode, ThemeProvider } from '@mui/material';
import { BrowserRouter } from 'react-router-dom';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';

import './App.css';
import { CookieKeys } from './shared/CookieKeys';
import { getThemeDesignTokens } from './shared/views/ThemeRelated';
import MasterLayout from './views/MasterLayout';
import { useEffect } from 'react';
import { setGeoLocation } from './shared/slices/appSlice';
import axios from 'axios';
import { useDispatch } from 'react-redux';

function App() {
    const dispatch = useDispatch();
    
    let mode = localStorage.getItem(CookieKeys.Theme);
    mode = mode === 'dark' ? 'dark' : 'light';
    const currentTheme = createTheme(getThemeDesignTokens(mode as PaletteMode));

    const geGeoLocation = async () => {
        const res = await axios.get('https://geolocation-db.com/json/')
        console.log(res.data);
        setGeoLocation(res.data);
    }

    useEffect(() => {
        //passing getData method to the lifecycle method
        //geGeoLocation()

    }, [])
    
    return (
        <div className="App">
            <BrowserRouter>
                <LocalizationProvider dateAdapter={AdapterMoment}>
                    <ThemeProvider theme={currentTheme}>
                        <CssBaseline />
                        <MasterLayout />
                    </ThemeProvider>
                </LocalizationProvider>
            </BrowserRouter>
        </div>
    );
}

export default App;
