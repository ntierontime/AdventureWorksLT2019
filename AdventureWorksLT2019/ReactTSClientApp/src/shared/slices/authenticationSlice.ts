// https://stackoverflow.com/questions/62020623/createasyncthunk-and-writing-reducer-login-with-redux-toolkit
// https://stackoverflow.com/questions/64287428/call-reducer-from-a-different-slice-redux-toolkit

import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

import Cookies from "universal-cookie";

import { CookieKeys } from "src/shared/CookieKeys";

import { LoginViewModel } from "src/shared/viewModels/LoginViewModel";
import { authenticationApi } from "src/apiClients/AuthenticationApi";
import { RegisterViewModel } from "src/shared/viewModels/RegisterViewModel";

//TODO: username and/or password should be kept in cookie/localStorage
export const login = createAsyncThunk(
    'login',
    async ({ email, password, from }: LoginViewModel, { dispatch }) => {
        // TODO: the following link can authenticate with Asp.Net Core built in Identity Framework
        const response = await authenticationApi.login({ email: email, password: password, rememberMe: true, from });
        // const response = {
        //     succeeded: true,
        //     isLockedOut: false,
        //     isNotAllowed: false,
        //     requiresTwoFactor: false,
        //     token: 'Fake Token',
        //     expiresIn: 7,
        //     refreshToken: 'Fake Rereshed Token',
        //     entityID: 'null',
        //     roles: ['admin']
        // }
        localStorage.setItem('user', JSON.stringify(response));

        if (response.succeeded === true) {
            const cookies = new Cookies();
            cookies.set(CookieKeys.Token, response.token);
        }

        return response;
    }
)

export const autoLogIn = createAsyncThunk(
    'autologin',
    async () => {
        const loggedInUser = localStorage.getItem("user");
        if (loggedInUser) {
            const foundUser = JSON.parse(loggedInUser);
            return foundUser;
        }
    }
)

export const logout = createAsyncThunk(
    'logout',
    async () => {
        const loggedInUser = localStorage.getItem("user");
        if (loggedInUser) {
            const foundUser = JSON.parse(loggedInUser);
            const response = await authenticationApi.logout(foundUser);
            if (response.succeeded) {
                localStorage.removeItem('user');
                const cookies = new Cookies();
                cookies.set(CookieKeys.Token, null);

                new Promise(r => setTimeout(r, 1000));
                return;
            }
        }

        // TODO: should add some alert here to say "Logout Failed"

    }
)

export const register = createAsyncThunk(
    'register',
    async ({ email, password, confirmPassword }: RegisterViewModel, { dispatch }) => {
        // TODO: the following link can authenticate with Asp.Net Core built in Identity Framework
        const response = await authenticationApi.register({ email: email, password: password, confirmPassword: confirmPassword });
        // const response = {
        //     succeeded: true,
        //     isLockedOut: false,
        //     isNotAllowed: false,
        //     requiresTwoFactor: false,
        //     token: 'Fake Token',
        //     expiresIn: 7,
        //     refreshToken: 'Fake Rereshed Token',
        //     entityID: 'null',
        //     roles: ['admin']
        // }
        localStorage.setItem('user', JSON.stringify(response));

        if (response.succeeded === true) {
            const cookies = new Cookies();
            cookies.set(CookieKeys.Token, response.token);
        }

        return response;
    }
)
const authSlice = createSlice({
    name: "authSlice",
    initialState: {
        isLoggingIn: false,
        isLoggingOut: false,
        isVerifying: false,
        loginError: false,
        logoutError: false,
        isAuthenticated: false,
        token: null,
        user: {},
    },
    reducers: {
        /* any other state updates here */
        setIsAuthenticated(state) {
            state.isAuthenticated = true;
        },
    },
    extraReducers: builder => {
        builder.addCase(login.pending, (state) => {
            state.isLoggingIn = true;
            state.isAuthenticated = false;
            state.loginError = false;
            // console.log("login.pending");
        });
        builder.addCase(login.fulfilled, (state, { payload }) => {
            state.isAuthenticated = true;
            state.isLoggingIn = false;
            state.loginError = false;
            state.token = payload.token;
            // console.log("login.fulfilled");
        });
        builder.addCase(login.rejected, (state, action) => {
            state.isLoggingIn = false;
            state.isAuthenticated = false;
            state.loginError = true;
            state.token = null;
            // console.log("login.rejected");
        });
        builder.addCase(autoLogIn.pending, (state) => {
            state.isLoggingIn = true;
            state.isAuthenticated = false;
            state.loginError = false;
            // console.log("autoLogIn.pending");
        });
        builder.addCase(autoLogIn.fulfilled, (state, { payload }) => {
            if (!!payload) {
                state.isAuthenticated = true;
                state.loginError = false;
                // console.log("autoLogIn.fulfilled");
            }
            state.isLoggingIn = false;
        });
        builder.addCase(autoLogIn.rejected, (state, action) => {
            state.isAuthenticated = false;
            state.isLoggingIn = false;
            state.loginError = true;
            state.token = null;
            // console.log("autoLogIn.rejected");
        });
        builder.addCase(logout.pending, (state) => {
            state.isLoggingOut = true;
            // console.log("logout.pending");
        });
        builder.addCase(logout.fulfilled, (state, { payload }) => {
            state.isLoggingOut = false;
            state.isAuthenticated = false;
            state.logoutError = false;
            state.token = null;
            // console.log("logout.fulfilled");
        });
        builder.addCase(logout.rejected, (state, action) => {
            state.isLoggingOut = false;
            state.logoutError = true;
            // console.log("logout.rejected");
        });
    }
});

export const { setIsAuthenticated } = authSlice.actions;
export default authSlice.reducer;

