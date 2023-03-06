import { IconButton, IconButtonProps, styled } from "@mui/material";

export interface ExpandMoreIconButtonProps extends IconButtonProps {
    expand: boolean;
}

export const ExpandMoreIconButton = styled((props: ExpandMoreIconButtonProps) => {
    const { expand, ...other } = props;
    return <IconButton {...other} />;
})(({ theme, expand }) => ({
    transform: !expand ? 'rotate(0deg)' : 'rotate(180deg)',
    marginLeft: 'auto',
    transition: theme.transitions.create('transform', {
        duration: theme.transitions.duration.shortest,
    }),
}));