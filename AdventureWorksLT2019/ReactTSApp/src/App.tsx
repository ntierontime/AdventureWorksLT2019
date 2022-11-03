import { createTheme, CssBaseline, PaletteMode, ThemeProvider } from '@mui/material';
import { BrowserRouter } from 'react-router-dom';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';

import './App.css';
import { CookieKeys } from './shared/CookieKeys';
import { getThemeDesignTokens } from './shared/views/ThemeRelated';
import MasterLayout from './views/MasterLayout';

function App() {
    let mode = localStorage.getItem(CookieKeys.Theme);
    mode = mode === 'dark' ? 'dark' : 'light';
    const currentTheme = createTheme(getThemeDesignTokens(mode as PaletteMode));

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
