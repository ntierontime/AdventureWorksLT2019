import { Button } from "@mui/material";

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
import VisibilityIcon from "@mui/icons-material/Visibility";
import VisibilityOutlinedIcon from "@mui/icons-material/VisibilityOutlined";

import { useTranslation } from "react-i18next";

import BooleanStatusIconButtonProps from "./BooleanStatusIconButtonProps";

// // 1.1. No IconAndTextButton: Like Entity or Item or EntityAlbumItem
// export function LikeStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
//     const { identifier, size, value, submitting, onChange } = props;

//     return (
//         <>
//             {!!value && (
//                 <IconButton
//                     aria-label="create"
//                     color="primary"
//                     size={size}
//                     disabled={submitting}
//                     onClick={() => {onChange(!value)}}
//                 >
//                     <ThumbUpIcon />
//                 </IconButton>
//             )}
//             {!!!value && (
//                 <IconButton
//                     aria-label="Like"
//                     disabled={submitting}
//                     size={size}
//                     onClick={() => {onChange(!value)}}
//                 >
//                     <ThumbUpOffAltIcon />
//                 </IconButton>
//             )}
//         </>
//     );
// }

// // 1.2. No IconAndTextButton: Unlike Entity or Item or EntityAlbumItem
// export function UnLikeStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
//     const { identifier, size, value, submitting, onChange } = props;

//     return (
//         <>
//             {!!value && (
//                 <IconButton
//                     aria-label="CancelUnlike"
//                     color="primary"
//                     size={size}
//                     disabled={submitting}
//                     onClick={() => {onChange(!value)}}
//                 >
//                     <ThumbDownIcon />
//                 </IconButton>
//             )}
//             {!!!value && (
//                 <IconButton
//                     aria-label="Unlike"
//                     size={size}
//                     disabled={submitting}
//                     onClick={() => {onChange(!value)}}
//                 >
//                     <ThumbDownOffAltIcon />
//                 </IconButton>
//             )}
//         </>
//     );
// }

// 1.3. ReportAbuse Entity or Item or EntityAlbumItem
export function ReportAbuseStatusIconAndTextButton(
    props: BooleanStatusIconButtonProps
): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="ReportAbuse"
            size={size}
            disabled={submitting || value}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            startIcon={value ? <FlagIcon /> : <FlagOutlinedIcon />}
        >
            {t(value ? "ReportedAbuse" : "ReportAbuse")}
        </Button>
    );
}

// 2.1. Bookmark Entity or Item
export function BookmarkStatusIconAndTextButton(
    props: BooleanStatusIconButtonProps
): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Bookmark"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            startIcon={value ? <BookmarkIcon /> : <BookmarkAddOutlinedIcon />}
        >
            {t(value ? "Bookmarked" : "Bookmark")}
        </Button>
    );
}

// 2.2. Favorite Entity or Item
export function FavoriteStatusIconAndTextButton(
    props: BooleanStatusIconButtonProps
): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Favorite"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            startIcon={value ? <FavoriteIcon /> : <FavoriteBorderIcon />}
        >
            {t("Favorite")}
        </Button>
    );
}

// 2.3. Recommend Entity or Item
export function RecommendStatusIconAndTextButton(
    props: BooleanStatusIconButtonProps
): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Recommend"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            startIcon={value ? <RecommendIcon /> : <RecommendOutlinedIcon />}
        >
            {t(value ? "Recommended" : "Recommend")}
        </Button>
    );
}

// 2.4. Watch Entity or Item
export function WatchStatusIconAndTextButton(
    props: BooleanStatusIconButtonProps
): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Watch"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            startIcon={value ? <VisibilityIcon /> : <VisibilityOutlinedIcon />}
            sx={{ width: "100%" }}
        >
            {t(value ? "Watching" : "Watch")}
        </Button>
    );
}

// 3.1. Connect Entity -- Entity to Entity can message
export function ConnectStatusIconAndTextButton(
    props: BooleanStatusIconButtonProps
): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Connect"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            startIcon={value ? <LinkIcon /> : <AddLinkIcon />}
        >
            {t(value ? "Connected" : "Connect")}
        </Button>
    );
}

// 3.2. Follow -- Entity: can see new Item or Sub Entity, e.g. new Employee of a Partner
export function FollowStatusIconAndTextButton(
    props: BooleanStatusIconButtonProps
): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Follow"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            startIcon={value ? <CheckIcon /> : <AddIcon />}
        >
            {t(value ? "Following" : "Follow")}
        </Button>
    );
}

// 3.3. Mute Entity, when Subscribe
export function MuteStatusIconAndTextButton(
    props: BooleanStatusIconButtonProps
): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Mute"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            startIcon={<HearingDisabledIcon />}
        >
            {t(value ? "Muted" : "Mute")}
        </Button>
    );
}

// 3.4. Block Entity, when Subscribe
export function BlockStatusIconAndTextButton(
    props: BooleanStatusIconButtonProps
): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Block"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            startIcon={<BlockIcon />}
        >
            {t(value ? "Blocked" : "Block")}
        </Button>
    );
}

// 3.5. Subscribe -- Entity Email notification any news from an Entity
export function SubscribeStatusIconAndTextButton(
    props: BooleanStatusIconButtonProps
): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;
    const { t } = useTranslation();

    return (
        <Button
            onClick={() => {
                onChange(identifier, !value);
            }}
            aria-label="Subscribe"
            size={size}
            disabled={submitting}
            color={value ? "primary" : "secondary"}
            variant={value ? "contained" : "text"}
            startIcon={
                value ? (
                    <CircleNotificationsIcon />
                ) : (
                    <CircleNotificationsOutlinedIcon />
                )
            }
        >
            {t(value ? "Subscribed" : "Subscribe")}
        </Button>
    );
}
