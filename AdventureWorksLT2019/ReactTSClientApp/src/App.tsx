import { createTheme, CssBaseline, PaletteMode, Theme, ThemeProvider, useMediaQuery } from '@mui/material';
import { BrowserRouter } from 'react-router-dom';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';

import './App.css';
import { getThemeDesignTokens } from './shared/views/ThemeRelated';
import MasterLayout from './views/MasterLayout';
import { useEffect, useState } from 'react';
import { setGeoLocation, setGettingTempToken } from './slices/appSlice';
import axios from 'axios';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from './store/CombinedReducers';
import Cookies from 'universal-cookie';
import { CookieKeys } from './shared/CookieKeys';
import { AppDispatch } from './store/Store';
import { getTempToken } from './slices/msIdentityFrameworkSlice';

function App() {
    const dispatch = useDispatch<AppDispatch>();
    const app = useSelector((state: RootState) => state.app);
    const auth = useSelector((state: RootState) => state.auth);
    
    const userPreference = useSelector((state: RootState) => state.userPreference);
    const { theme } = userPreference;

    const [currentTheme, setCurrentTheme] = useState<Theme>(createTheme(getThemeDesignTokens(theme as unknown as PaletteMode)))
    // const isBreakPoints_xs_A = useMediaQuery(`(min-width: ${currentTheme.breakpoints.values.xs}px)`);
    // const isBreakPoints_sm_A = useMediaQuery(`(min-width: ${currentTheme.breakpoints.values.sm}px)`);
    // const isBreakPoints_md_A = useMediaQuery(`(min-width: ${currentTheme.breakpoints.values.md}px)`);
    // const isBreakPoints_lg_A = useMediaQuery(`(min-width: ${currentTheme.breakpoints.values.lg}px)`);
    // const isBreakPoints_xl = useMediaQuery(`(min-width: ${currentTheme.breakpoints.values.xl}px`);
    // const isBreakPoints_xs = useMediaQuery(`(max-width: ${currentTheme.breakpoints.values.sm - 1}px`);
    // const isBreakPoints_sm = useMediaQuery(`(max-width: ${currentTheme.breakpoints.values.md - 1}px`);
    // const isBreakPoints_md = useMediaQuery(`(max-width: ${currentTheme.breakpoints.values.lg - 1}px`);
    // const isBreakPoints_lg = useMediaQuery(`(max-width: ${currentTheme.breakpoints.values.xl - 1}px`);
    // //const isPhoneScreen = useMediaQuery();
    // console.log(currentTheme.breakpoints);
    // console.log(isBreakPoints_xs_A && isBreakPoints_xs, isBreakPoints_sm_A && isBreakPoints_sm, isBreakPoints_md_A && isBreakPoints_md, isBreakPoints_lg_A && isBreakPoints_lg, isBreakPoints_xl);
    
    const successCallback = (position: any) => {
        if(!!!position) {
            return;
        }
        // if(!!!app.geoLocationFromNavigator || app.geoLocationFromNavigator.latitude !== position.latitude || app.geoLocationFromNavigator.longitude !== position.longitude ){
        //     setGeoLocationFromNavigator(position);
        //     //geGeoLocation();
        //     //console.log(position);
        // }
    };

    const errorCallback = (error: any) => {
        //console.log(error);
    };

    // navigator.geolocation.getCurrentPosition(successCallback, errorCallback);
    const id = navigator.geolocation.watchPosition(successCallback, errorCallback);
    //console.log("id", id);
    
    const geGeoLocation = async () => {
        const res = await axios.get('https://geolocation-db.com/json/')
        //console.log(res.data);
        setGeoLocation(res.data);
    }

    useEffect(() => {
        //passing getData method to the lifecycle method
        // eGeoLocation()
        const cookies = new Cookies();
        const token = cookies.get(CookieKeys.Token, { doNotParse: true });
        if((!!!auth || !!!auth.isAuthenticated) && (!!!token || token === 'null')) {
            setGettingTempToken(true);
            dispatch(getTempToken()).finally(() => { setGettingTempToken(false); });
        }
    }, [])
    
    useEffect(() => {
        setCurrentTheme(createTheme(getThemeDesignTokens(theme as unknown as PaletteMode)));
    }, [theme])

    return (
        <div className="App">
            <BrowserRouter>
                <LocalizationProvider dateAdapter={AdapterDayjs}>
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

