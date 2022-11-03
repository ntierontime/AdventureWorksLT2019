import { PaletteMode, Theme } from '@mui/material';
import { ItemUIStatus } from '../dataModels/ItemUIStatus';
export const getThemeDesignTokens = (mode: PaletteMode) => ({
    palette: {
        mode: mode,
    },
});

export function getAvatarStyle(itemUIStatus: ItemUIStatus, theme: Theme): { bgcolor: string, color: string } {
    if(itemUIStatus === ItemUIStatus.NoChange){
        return { bgcolor: theme.palette.info.main, color: theme.palette.background.default };
    }
    if(itemUIStatus === ItemUIStatus.Updated){
        return { bgcolor: theme.palette.warning.main, color: theme.palette.background.default };
    }
    // itemUIStatus === ItemUIStatus.Updated
    return { bgcolor: theme.palette.warning.main, color: theme.palette.background.default };
}