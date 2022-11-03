import * as React from 'react';
import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { styled } from '@mui/material/styles';
import { Button, Grid, Menu, MenuItem, PaletteMode, Popover } from '@mui/material';
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import { AccountCircle, Logout } from '@mui/icons-material';

import { useTranslation } from 'react-i18next';
import Cookies from 'universal-cookie';
import { CookieKeys } from 'src/shared/CookieKeys';
import i18next from 'i18next';

import { drawerWidth } from "../constants";
import { Item } from './Item';
import { supportedLngs } from 'src/i18n';
import { logout } from 'src/slices/authenticationSlice';
import { AppDispatch } from 'src/store/Store';
import { useNavigate } from 'react-router-dom';

interface AppBarProps extends MuiAppBarProps {
    open?: boolean;
    title?: string;
    openDrawerHandler?: () => void;
}

const StyledAppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open }) => ({
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    ...(open && {
        marginLeft: drawerWidth,
        width: `calc(100% - ${drawerWidth}px)`,
        transition: theme.transitions.create(['width', 'margin'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen,
        }),
    }),
}));

export default function AppBar(props: AppBarProps) {
    const dispatch = useDispatch<AppDispatch>();
    const navigate = useNavigate();

    // 1.start Open Profile
    const [anchorProfileEl, setProfileAnchorEl] = useState<HTMLButtonElement | null>(null);

    const handleProfileClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setProfileAnchorEl(event.currentTarget);
    };

    const handleProfileClose = () => {
        setProfileAnchorEl(null);
    };
    const openProfile = Boolean(anchorProfileEl);
    const id = openProfile ? 'simple-popover' : undefined;

    // 1.End Open Profile

    // 1.1.Start Change Language
    const [anchorElLanguage, setAnchorLanguage] = useState()
    const { t, i18n } = useTranslation();
    const openLanguage = Boolean(anchorElLanguage);
    const [language, setLanguage] = useState('')
    const [languages, setLanguages] = useState([])
    const changeLanguage = (language: string) => {
        i18n.changeLanguage(language);
        setLanguage(i18next.language);
        setAnchorLanguage(null);
		const cookies = new Cookies();
        cookies.set(CookieKeys.Language, i18next.language);
    }
    const handleLanguageOpen = (event: any) => {
        setAnchorLanguage(event.currentTarget);
    };

    const handleLanguageClose = (lang?: string) => {
        setAnchorLanguage(null);
    };
    // 1.1.End Change Language

    // 1.2.Start Change Language
    const [anchorElTheme, setAnchorTheme] = useState()
    const openTheme = Boolean(anchorElTheme);
    const [theme, setTheme] = useState('')
    const [themes, setThemes] = useState([])
    const handleChangeTheme = (theme1: PaletteMode) => {
        setTheme(theme1);
        setAnchorTheme(null);
        localStorage.setItem(CookieKeys.Theme, theme1);
        window.location.reload();
    }
    const handleThemeOpen = (event: any) => {
        setAnchorTheme(event.currentTarget);
    };

    const handleThemeClose = (lang?: string) => {
        setAnchorTheme(null);
    };
    // 1.2.End Change Theme

    // 1.3.Start Logout
    const handleLogout = () => {
        dispatch(logout());
        navigate("/");
    };

    useEffect(() => {
        // you can do async server request and fill up form
        // 1.1. Language
        setLanguages(supportedLngs);
        const cookies = new Cookies();
        const language = cookies.get(CookieKeys.Language);
        if(language)
        {
            setLanguage(language);
            i18n.changeLanguage(language);
        }
        else
        {
            setLanguage(i18n.language);
        }

        // 1.2. Theme
        setThemes(['light', 'dark']);
        const theme1 = localStorage.getItem(CookieKeys.Theme);
        if(theme1)
        {
            setTheme(theme1);
        }
        else
        {
            setTheme('dark');
        }
    }, [i18n]);

    return (
        <StyledAppBar position="fixed" open={props.open}>
            <Toolbar>
                <IconButton
                    color="inherit"
                    aria-label="open drawer"
                    onClick={props.openDrawerHandler}
                    edge="start"
                    sx={{
                        marginRight: 5,
                        ...(props.open && { display: 'none' }),
                    }}
                >
                    <MenuIcon />
                </IconButton>
                <Typography variant="h6" noWrap component="div" sx={{ flexGrow: 1 }}>
                    {props.title}
                </Typography>

                {/* <Button color="inherit">Login</Button>
                <Button color="inherit">Login</Button> */}
                <IconButton aria-label="{id}" size="large" onClick={handleProfileClick}>
                    <AccountCircle fontSize="inherit" />
                </IconButton>
                <Popover
                    id={id}
                    open={openProfile}
                    anchorEl={anchorProfileEl}
                    onClose={handleProfileClose}
                    anchorOrigin={{
                        vertical: 'bottom',
                        horizontal: 'left',
                    }}
                >
                    <Grid container spacing={2} padding={2}>
                        {/* 1.1. Languages */}
                        <Grid item xs={4}>
                            <Item>{t('Language')}</Item>
                        </Grid>
                        <Grid item xs={8}>
                            <Item>
                                <IconButton
                                    aria-owns={openLanguage ? 'menu-appbar' : null}
                                    aria-haspopup="true"
                                    onClick={handleLanguageOpen}
                                    color="inherit">
                                    {language}
                                </IconButton>
                                <Menu
                                    id="language-appbar"
                                    anchorEl={anchorElLanguage}
                                    anchorOrigin={{
                                        vertical: 'top',
                                        horizontal: 'right',
                                    }}
                                    transformOrigin={{
                                        vertical: 'top',
                                        horizontal: 'right',
                                    }}
                                    open={openLanguage}
                                    onClose={(e) => handleLanguageClose(null)}
                                >
                                    {languages.map((lang: string) => {
                                        return (
                                            <MenuItem key={lang} onClick={(e) => changeLanguage(lang)}>{lang}</MenuItem>
                                        );
                                    })}
                                </Menu>
                            </Item>
                        </Grid>
                        {/* 1.2. Themes */}
                        <Grid item xs={4}>
                            <Item>{t('Theme')}</Item>
                        </Grid>
                        <Grid item xs={8}>
                            <Item>
                                <IconButton
                                    aria-owns={openTheme ? 'menu-appbar' : null}
                                    aria-haspopup="true"
                                    onClick={handleThemeOpen}
                                    color="inherit"
                                >
                                    {theme}
                                </IconButton>
                                <Menu
                                    id="language-appbar"
                                    anchorEl={anchorElTheme}
                                    anchorOrigin={{
                                        vertical: 'top',
                                        horizontal: 'right',
                                    }}
                                    transformOrigin={{
                                        vertical: 'top',
                                        horizontal: 'right',
                                    }}
                                    open={openTheme}
                                    onClose={(e) => handleThemeClose(null)}
                                >
                                    {themes.map((theme1: PaletteMode) => {
                                        return (
                                            <MenuItem key={theme1} onClick={(e) => handleChangeTheme(theme1)}>{theme1}</MenuItem>
                                        );
                                    })}
                                </Menu>
                                </Item>
                        </Grid>
                        <Grid item xs={12}>
                            <Item><Button variant="text" startIcon={<Logout />} onClick={(e) => handleLogout()}>
                                {t('LogOut')}
                            </Button></Item>
                        </Grid>
                    </Grid>
                </Popover>
            </Toolbar>
        </StyledAppBar>
    );
}