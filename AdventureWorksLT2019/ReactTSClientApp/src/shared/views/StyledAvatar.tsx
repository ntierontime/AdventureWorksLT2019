import { Avatar } from "@mui/material";

export interface StyledAvatarProps {
    alt: string;
    imageSrc?: string;
    text: string;
    height?: number;
    width?: number;
    bgcolor?: any;
}

export default function StyledAvatar(props: StyledAvatarProps): JSX.Element {
    const {
        alt,
        imageSrc,
        text,
        height = 36,
        width = 36,
        bgcolor
    } = props;


    return (
        <Avatar
            alt={alt}
            src={imageSrc}
            sx={{ height: height, width: width, bgcolor: bgcolor }}
        >{text}
        </Avatar>
    );
}