import {
    MD2Theme,
    MD3Theme,
    useTheme,
} from 'react-native-paper';
import { ItemUIStatus } from '../dataModels/ItemUIStatus';

export const useExampleTheme = () => useTheme<MD2Theme | MD3Theme>();

export function getAvatarStyle(itemUIStatus: ItemUIStatus, theme: MD2Theme | MD3Theme): { bgcolor: string, color: string } {
    if (theme.isV3) {
        if (itemUIStatus === ItemUIStatus.NoChange) {
            return { bgcolor: theme.colors.primary, color: theme.colors.primaryContainer };
        }
        if (itemUIStatus === ItemUIStatus.Updated) {
            return { bgcolor: theme.colors.error, color: theme.colors.errorContainer };
        }
        // itemUIStatus === ItemUIStatus.Updated
        return { bgcolor: theme.colors.tertiary, color: theme.colors.tertiaryContainer };
    }
    if (itemUIStatus === ItemUIStatus.NoChange) {
        return { bgcolor: theme.colors.primary, color: theme.colors.background };
    }
    if (itemUIStatus === ItemUIStatus.Updated) {
        return { bgcolor: theme.colors.error, color: theme.colors.background };
    }
    // itemUIStatus === ItemUIStatus.Updated
    return { bgcolor: theme.colors.onSurface, color: theme.colors.background };
}