import { Button, IconButton, IconButtonPropsSizeOverrides, ListItemIcon, ListItemText, MenuItem } from "@mui/material";

import AddIcon from "@mui/icons-material/Add";
import AddLinkIcon from "@mui/icons-material/AddLink";
import BlockIcon from '@mui/icons-material/Block';
import BookmarkIcon from "@mui/icons-material/Bookmark";
import BookmarkAddOutlinedIcon from "@mui/icons-material/BookmarkAddOutlined";
import CheckIcon from "@mui/icons-material/Check";
import CircleNotificationsIcon from "@mui/icons-material/CircleNotifications";
import CircleNotificationsOutlinedIcon from "@mui/icons-material/CircleNotificationsOutlined";
import FavoriteBorderIcon from "@mui/icons-material/FavoriteBorder";
import FavoriteIcon from "@mui/icons-material/Favorite";
import FlagIcon from '@mui/icons-material/Flag';
import FlagOutlinedIcon from '@mui/icons-material/FlagOutlined';
import HearingDisabledIcon from '@mui/icons-material/HearingDisabled';
import LinkIcon from "@mui/icons-material/Link";
import RecommendIcon from "@mui/icons-material/Recommend";
import RecommendOutlinedIcon from "@mui/icons-material/RecommendOutlined";
import ThumbDownOffAltIcon from '@mui/icons-material/ThumbDownOffAlt';
import ThumbDownIcon from '@mui/icons-material/ThumbDown';
import ThumbUpOffAltIcon from '@mui/icons-material/ThumbUpOffAlt';
import ThumbUpIcon from '@mui/icons-material/ThumbUp';
import VisibilityIcon from "@mui/icons-material/Visibility";
import VisibilityOutlinedIcon from "@mui/icons-material/VisibilityOutlined";
import { OverridableStringUnion } from "@mui/types";

import { useTranslation } from "react-i18next";

import BooleanStatusIconButtonProps from "./BooleanStatusIconButtonProps";

// // 1.1. Like Entity or Item or EntityAlbumItem
// export function LikeStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
//     const { identifier, size, value, submitting, onChange } = props;

//     return (
//         <>
//             {!!value && (
//                 <IconButton
//                     aria-label="create"
//                     color="primary"
//                     size={size}
//                     disabled={submitting}
//                     onClick={() => {onChange(identifier, !value);}}
//                 >
//                     <ThumbUpIcon />
//                 </IconButton>
//             )}
//             {!!!value && (
//                 <IconButton
//                     aria-label="Like"
//                     disabled={submitting}
//                     size={size}
//                     onClick={() => {onChange(identifier, !value);}}
//                 >
//                     <ThumbUpOffAltIcon />
//                 </IconButton>
//             )}
//         </>
//     );
// }

// // 1.2. Unlike Entity or Item or EntityAlbumItem
// export function UnLikeStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
//     const { identifier, size, value, submitting, onChange } = props;

//     return (
//         <>
//             {!!value && (
//                 <IconButton
//                     aria-label="CancelUnlike"
//                     color="primary"
//                     size={size}
//                     disabled={submitting}
//                     onClick={() => {onChange(identifier, !value);}}
//                 >
//                     <ThumbDownIcon />
//                 </IconButton>
//             )}
//             {!!!value && (
//                 <IconButton
//                     aria-label="Unlike"
//                     size={size}
//                     disabled={submitting}
//                     onClick={() => {onChange(identifier, !value);}}
//                 >
//                     <ThumbDownOffAltIcon />
//                 </IconButton>
//             )}
//         </>
//     );
// }

// 1.3. ReportAbuse Entity or Item or EntityAlbumItem
export function ReportAbuseStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (<MenuItem 
        sx={{
            width: "250px",
            backgroundColor: value ? "primary" : "",
        }}
        //onClick={handleClose}
    >
        <ListItemIcon>
            {
                value ? <FlagIcon /> : <FlagOutlinedIcon />
            }
        </ListItemIcon>
        <ListItemText></ListItemText>
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="ReportAbuse"
            size={size}
            disabled={submitting || value}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            sx={{ width: "100%" }}
        >
            {t(value ? "ReportedAbuse" : "ReportAbuse")}
        </Button>
    </MenuItem>);
}

// 2.1. Bookmark Entity or Item
export function BookmarkStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (<MenuItem 
        sx={{
            width: "250px",
            backgroundColor: value ? "primary" : "",
        }}
        //onClick={handleClose}
    >
        <ListItemIcon>
            {
                value ? <BookmarkIcon /> : <BookmarkAddOutlinedIcon />
            }
        </ListItemIcon>
        <ListItemText></ListItemText>
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Bookmark"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            sx={{ width: "100%" }}
        >
            {t(value ? "Bookmarked" : "Bookmark")}
        </Button>
    </MenuItem>);
}

// 2.2. Favorite Entity or Item
export function FavoriteStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (<MenuItem 
        sx={{
            width: "250px",
            backgroundColor: value ? "primary" : "",
        }}
        //onClick={handleClose}
    >
        <ListItemIcon>
            {
                value ? <FavoriteIcon /> : <FavoriteBorderIcon />
            }
        </ListItemIcon>
        <ListItemText></ListItemText>
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Favorite"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            sx={{ width: "100%" }}
        >
            {t("Favorite")}
        </Button>
    </MenuItem>);
}

// 2.3. Recommend Entity or Item
export function RecommendStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (<MenuItem 
        sx={{
            width: "250px",
            backgroundColor: value ? "primary" : "",
        }}
        //onClick={handleClose}
    >
        <ListItemIcon>
            {
                value ? <RecommendIcon /> : <RecommendOutlinedIcon />
            }
        </ListItemIcon>
        <ListItemText></ListItemText>
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Recommend"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            sx={{ width: "100%" }}
        >
            {t(value ? "Recommended" : "Recommend")}
        </Button>
    </MenuItem>);
}

// 2.4. Watch Entity or Item
export function WatchStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (<MenuItem 
        sx={{
            width: "250px",
            backgroundColor: value ? "primary" : "",
        }}
        //onClick={handleClose}
    >
        <ListItemIcon>
            {
                value ? <VisibilityIcon /> : <VisibilityOutlinedIcon />
            }
        </ListItemIcon>
        <ListItemText></ListItemText>
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Watch"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            sx={{ width: "100%" }}
        >
            {t(value ? "Watching" : "Watch")}
        </Button>
    </MenuItem>);
}

// 3.1. Connect Entity -- Entity to Entity can message
export function ConnectStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (<MenuItem 
        sx={{
            width: "250px",
            backgroundColor: value ? "primary" : "",
        }}
        //onClick={handleClose}
    >
        <ListItemIcon>
            {
                value ? <LinkIcon /> : <AddLinkIcon />
            }
        </ListItemIcon>
        <ListItemText></ListItemText>
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Connect"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            sx={{ width: "100%" }}
        >
            {t(value ? "Connected" : "Connect")}
        </Button>
    </MenuItem>);
}

// 3.2. Follow -- Entity: can see new Item or Sub Entity, e.g. new Employee of a Partner
export function FollowStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (<MenuItem 
        sx={{
            width: "250px",
            backgroundColor: value ? "primary" : "",
        }}
        //onClick={handleClose}
    >
        <ListItemIcon>
            {
                value ? <CheckIcon /> : <AddIcon />
            }
        </ListItemIcon>
        <ListItemText></ListItemText>
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Follow"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            sx={{ width: "100%" }}
        >
            {t(value ? "Following" : "Follow")}
        </Button>
    </MenuItem>);

}

// 3.3. Mute Entity, when Subscribe
export function MuteStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (<MenuItem 
        sx={{
            width: "250px",
            backgroundColor: value ? "primary" : "",
        }}
        //onClick={handleClose}
    >
        <ListItemIcon>
            <HearingDisabledIcon />
        </ListItemIcon>
        <ListItemText></ListItemText>
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Mute"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            sx={{ width: "100%" }}
        >
            {t(value ? "Muted" : "Mute")}
        </Button>
    </MenuItem>);
}

// 3.4. Block Entity, when Subscribe
export function BlockStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (<MenuItem 
        sx={{
            width: "250px",
            backgroundColor: value ? "primary" : "",
        }}
        //onClick={handleClose}
    >
        <ListItemIcon><BlockIcon /></ListItemIcon>
        <ListItemText></ListItemText>
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Block"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            sx={{ width: "100%" }}
        >
            {t(value ? "Blocked" : "Block")}
        </Button>
    </MenuItem>);
}

// 3.5. Subscribe -- Entity Email notification any news from an Entity
export function SubscribeStatusMenuItem(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (<MenuItem 
        sx={{
            width: "250px",
            backgroundColor: value ? "primary" : "",
        }}
        //onClick={handleClose}
    >
        <ListItemIcon>
            {
                value ? <CircleNotificationsIcon /> : <CircleNotificationsOutlinedIcon />
            }
        </ListItemIcon>
        <ListItemText></ListItemText>
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Subscribe"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            sx={{ width: "100%" }}
        >
            {t(value ? "Subscribed" : "Subscribe")}
        </Button>
    </MenuItem>);
}
