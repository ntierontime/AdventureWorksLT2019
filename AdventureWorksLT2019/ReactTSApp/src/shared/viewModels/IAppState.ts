import { AppDrawerItem } from "./AppDrawerItem";

export interface IAppState {
    // 1.1 AppBar
    appBarTitle: string;
    // 1.2 AppDrawer
    drawerOpen: boolean;
    // 2.1 Loading popup
    loading: boolean;
    // 3.1. 
    appDrawerItems: AppDrawerItem[];
}

export const initialState: IAppState = {
    appBarTitle: '',
    drawerOpen: true,
    loading: false,
    appDrawerItems: [],
}