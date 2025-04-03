import { IconButton, IconButtonPropsSizeOverrides } from "@mui/material";

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
import BooleanStatusIconButtonProps from "./BooleanStatusIconButtonProps";

// 1.1. Like Entity or Item or EntityAlbumItem
export function LikeStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <>
            {!!value && (
                <IconButton
                    aria-label="create"
                    color="primary"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <ThumbUpIcon />
                </IconButton>
            )}
            {!!!value && (
                <IconButton
                    aria-label="Like"
                    disabled={submitting}
                    size={size}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <ThumbUpOffAltIcon />
                </IconButton>
            )}
        </>
    );
}

// 1.2. Unlike Entity or Item or EntityAlbumItem
export function UnLikeStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <>
            {!!value && (
                <IconButton
                    aria-label="CancelUnlike"
                    color="primary"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <ThumbDownIcon />
                </IconButton>
            )}
            {!!!value && (
                <IconButton
                    aria-label="Unlike"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <ThumbDownOffAltIcon />
                </IconButton>
            )}
        </>
    );
}

// 1.3. ReportAbuse Entity or Item or EntityAlbumItem
export function ReportAbuseStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <>
            {!!value && (
                // can't cancel ReportAbuse
                <IconButton
                    aria-label="ReportedAbuse"
                    color="error"
                    size={size}
                    disabled={true}
                >
                    <FlagIcon />
                </IconButton>
            )}
            {!!!value && (
                <IconButton
                    aria-label="ReportedAbuse"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <FlagOutlinedIcon />
                </IconButton>
            )}
        </>
    );
}

// 2.1. Bookmark Entity or Item
export function BookmarkStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <>
            {!!value && (
                <IconButton
                    aria-label="CancelBookmarked"
                    color="primary"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <BookmarkIcon />
                </IconButton>
            )}
            {!!!value && (
                <IconButton
                    aria-label="Bookmark"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <BookmarkAddOutlinedIcon />
                </IconButton>
            )}
        </>
    );
}

// 2.2. Favorite Entity or Item
export function FavoriteStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <>
            {!!value && (
                <IconButton
                    aria-label="CancelRecommend"
                    color="primary"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <FavoriteIcon />
                </IconButton>
            )}
            {!!!value && (
                <IconButton
                    aria-label="Recommend"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <FavoriteBorderIcon />
                </IconButton>
            )}
        </>
    );
}

// 2.3. Recommend Entity or Item
export function RecommendStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <>
            {!!value && (
                <IconButton
                    aria-label="CancelRecommend"
                    color="primary"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <RecommendIcon />
                </IconButton>
            )}
            {!!!value && (
                <IconButton
                    aria-label="Recommend"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <RecommendOutlinedIcon />
                </IconButton>
            )}
        </>
    );
}

// 2.4. Watch Entity or Item
export function WatchStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <>
            {!!value && (
                <IconButton
                    aria-label="CancelWatch"
                    color="primary"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <VisibilityIcon />
                </IconButton>
            )}
            {!!!value && (
                <IconButton
                    aria-label="Watch"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <VisibilityOutlinedIcon />
                </IconButton>
            )}
        </>
    );
}

// 3.1. Connect Entity -- Entity to Entity can message
export function ConnectStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <>
            {!!value && (
                <IconButton
                    aria-label="CancelConnect"
                    color="primary"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <LinkIcon />
                </IconButton>
            )}
            {!!!value && (
                <IconButton
                    aria-label="Connect"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <AddLinkIcon />
                </IconButton>
            )}
        </>
    );
}

// 3.2. Follow -- Entity: can see new Item or Sub Entity, e.g. new Employee of a Partner
export function FollowStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <>
            {!!value && (
                <IconButton
                    aria-label="CancelFollow"
                    color="primary"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <CheckIcon />
                </IconButton>
            )}
            {!!!value && (
                <IconButton
                    aria-label="Follow"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <AddIcon />
                </IconButton>
            )}
        </>
    );
}

// 3.3. Mute Entity, when Subscribe
export function MuteStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <IconButton
            aria-label="Mute"
            color={value ? "primary" : "default"}
            size={size}
            disabled={submitting}
            onClick={() => { onChange(identifier, !value); }}
        >
            <HearingDisabledIcon />
        </IconButton>
    );
}

// 3.4. Block Entity, when Subscribe
export function BlockStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <IconButton
            aria-label="Block"
            color={value ? "primary" : "default"}
            size={size}
            disabled={submitting}
            onClick={() => { onChange(identifier, !value); }}
        >
            <BlockIcon />
        </IconButton>
    );
}

// 3.5. Subscribe -- Entity Email notification any news from an Entity
export function SubscribeStatusIconButton(props: BooleanStatusIconButtonProps): JSX.Element {
    const { identifier, size, value, submitting, onChange } = props;

    return (
        <>
            {!!value && (
                <IconButton
                    aria-label="CancelSubscribe"
                    color="primary"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <CircleNotificationsIcon />
                </IconButton>
            )}
            {!!!value && (
                <IconButton
                    aria-label="Subscribe"
                    size={size}
                    disabled={submitting}
                    onClick={() => {onChange(identifier, !value);}}
                >
                    <CircleNotificationsOutlinedIcon />
                </IconButton>
            )}
        </>
    );
}
