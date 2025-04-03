import styled from "@emotion/styled";
import { Paper } from "@mui/material";

export const Item = styled(Paper)(({ theme }: any) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(0.5),
    textAlign: 'center',
    color: theme.palette.text.secondary,
}));