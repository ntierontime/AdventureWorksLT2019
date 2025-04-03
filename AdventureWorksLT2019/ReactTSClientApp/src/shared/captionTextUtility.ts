import { Theme } from '@mui/material';
// usage: load all compare results from api, can be filtered by some parameters, the result should be filtered by indexes of the record.   
export function getTitle(input: string[]): string {
    if(!!!input || input.length === 0) {
        return null;
    }
    return input.join(" ");
}
