import { Theme } from '@mui/material';
import { ItemUIStatus } from "./dataModels/ItemUIStatus";

// usage: load all compare results from api, can be filtered by some parameters, the result should be filtered by indexes of the record.   
export function getAvatar(input: string[]) {
    if (!!!input || input.length === 0) {
        return "?";
    }

    const filtered = input.filter(v => !!v && v.length > 0);
    if (!!!filtered || filtered.length === 0) {
        return "?";
    }

    const result = filtered.map(v => v[0]).join('');
    return result;
}

export function getAvatarStyle(itemUIStatus: ItemUIStatus, theme: Theme): { bgcolor: string, color: string } {
    if (itemUIStatus === ItemUIStatus.NoChange) {
        return { bgcolor: theme.palette.info.main, color: theme.palette.background.default };
    }
    if (itemUIStatus === ItemUIStatus.Updated) {
        return { bgcolor: theme.palette.warning.main, color: theme.palette.background.default };
    }
    // itemUIStatus === ItemUIStatus.Updated
    return { bgcolor: theme.palette.warning.main, color: theme.palette.background.default };
}

export function stringToColor(string: string) {
    let hash = 0;
    let i;

    /* eslint-disable no-bitwise */
    for (i = 0; i < string.length; i += 1) {
        hash = string.charCodeAt(i) + ((hash << 5) - hash);
    }

    let color = '#';

    for (i = 0; i < 3; i += 1) {
        const value = (hash >> (i * 8)) & 0xff;
        color += `00${value.toString(16)}`.slice(-2);
    }
    /* eslint-enable no-bitwise */

    return color;
}

export function stringAvatar(name: string) {
    const children = name.indexOf(' ') === -1
        ? `${name[0]}${name[1]}`
        : `${name.split(' ')[0][0]}${name.split(' ')[1][0]}`;

    return {
        sx: {
            bgcolor: stringToColor(name),
            m: 1,
        },
        children: children,
    };
}

export const typoGraphyMaxLength = 200;
export function textShorten(input: string, length?: number) {
    const theLength = length ?? typoGraphyMaxLength;
    return !!!theLength || theLength <= 10 || input.length <= theLength 
        ? input
        : input.substring(0, theLength - 3) + "...";
}