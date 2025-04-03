import { ILabelValuePair } from "./dataModels/ILabelValuePair";
import { PaginationOptions } from "./dataModels/PaginationOptions";

export const drawerWidth = 240;
export const TimePickerStartDate = '2000-01-01';

export const pageSizeTiles = [
    {
        label: "12Items", value: 12
    },
    {
        label: "24Items", value: 24
    },
    {
        label: "48Items", value: 48
    },
    {
        label: "98Items", value: 98
    },
] as ILabelValuePair[];

export const sx_Toolbar = {
    m: { sm: 0 },
    pt: { sm: 0 },
    pb: { sm: 0 },
    pl: { sm: 0.5 },
    pr: { sm: 0.5 },
    // ...(numSelected > 0 && {
    bgcolor: 'transparent',
    // }),
    width: '100%',
};

export const sx_Toolbar_Box_flex = {
    m: { sm: 0 },
    p: { sm: 0 },
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    bgcolor: 'transparent',
    borderRadius: 1,
    width: '100%',
}