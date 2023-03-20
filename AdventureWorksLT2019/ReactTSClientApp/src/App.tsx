import { createTheme, CssBaseline, PaletteMode, Theme, ThemeProvider } from '@mui/material';
import { BrowserRouter } from 'react-router-dom';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';

import './App.css';
import { CookieKeys } from './shared/CookieKeys';
import { getThemeDesignTokens } from './shared/views/ThemeRelated';
import MasterLayout from './views/MasterLayout';
import { useEffect, useState } from 'react';
import { setGeoLocation, setLoading } from './slices/appSlice';
import axios from 'axios';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from './store/CombinedReducers';
import i18n from './i18n';

function App() {
    const dispatch = useDispatch();
    const userPreference = useSelector((state: RootState) => state.userPreference);
    const { language, theme } = userPreference;

    const [currentTheme, setCurrentTheme] = useState<Theme>(createTheme(getThemeDesignTokens(theme as unknown as PaletteMode)))

    // const currentTheme = createTheme(getThemeDesignTokens(theme as unknown as PaletteMode));

    const geGeoLocation = async () => {
        const res = await axios.get('https://geolocation-db.com/json/')
        console.log(res.data);
        setGeoLocation(res.data);
    }

    useEffect(() => {
        //passing getData method to the lifecycle method
        //geGeoLocation()
    }, [])
    
    useEffect(() => {
        setCurrentTheme(createTheme(getThemeDesignTokens(theme as unknown as PaletteMode)));
    }, [theme])

    useEffect(() => {
        i18n.changeLanguage(language);
    }, [language])
    
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
