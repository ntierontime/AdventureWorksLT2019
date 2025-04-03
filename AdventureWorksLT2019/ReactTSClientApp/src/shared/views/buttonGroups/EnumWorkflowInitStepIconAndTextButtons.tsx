import * as React from 'react';
import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";
import { Link as RouterLink, useNavigate } from 'react-router-dom';

import StartIcon from '@mui/icons-material/Start';
import EventAvailableIcon from '@mui/icons-material/EventAvailable';
import PersonAddIcon from '@mui/icons-material/PersonAdd';
// import PersonAddAltIcon from '@mui/icons-material/PersonAddAlt';
import GroupAddIcon from '@mui/icons-material/GroupAdd';
import ShoppingCartCheckoutIcon from '@mui/icons-material/ShoppingCartCheckout';

// ShoppingCart
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';

// AddToCart
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart';

// RemoveFromShoppngCart
import RemoveShoppingCartIcon from '@mui/icons-material/RemoveShoppingCart';

// WriteAReview
import RateReviewIcon from '@mui/icons-material/RateReview';

// AddComment
import AddCommentIcon from '@mui/icons-material/AddComment';

// Register

// Join

// BookNow

import { GenericButtonProps, RouteButtonProps } from './ButtonProps';
import { buildFullUrl } from 'src/shared/urlUtility';

// 1.1. Register
export function RegisterIconAndTextRouteButton(
    props: RouteButtonProps
): JSX.Element {
    const { pageUrl, routeParams, queryStringParams, size } = props;
    const { t } = useTranslation();
    const url = buildFullUrl(pageUrl, routeParams, queryStringParams, null);
    return (
        <Button component={RouterLink} to={url}
            aria-label="Register"
            size={size}
            color="primary"
            variant="contained"
            startIcon={<StartIcon />}
        >
            {t("Register")}
        </Button>
    );
}

// 1.2. Register
export function RegisterIconAndTextButton(
    props: GenericButtonProps
): JSX.Element {
    const { componentAttrs, size } = props;
    const { t } = useTranslation();
    return (
        <Button {...componentAttrs}
            aria-label="Register"
            size={size}
            color="primary"
            variant="contained"
            startIcon={<StartIcon />}
        >
            {t("Register")}
        </Button>
    );
}


// 2.1. BookNow
export function BookNowIconAndTextRouteButton(
    props: RouteButtonProps
): JSX.Element {
    const { pageUrl, routeParams, queryStringParams, size } = props;
    const { t } = useTranslation();
    const url = buildFullUrl(pageUrl, routeParams, queryStringParams, null);
    return (
        <Button component={RouterLink} to={url}
            aria-label="BookNow"
            size={size}
            color="primary"
            variant="contained"
            startIcon={<EventAvailableIcon />}
        >
            {t("BookNow")}
        </Button>
    );
}

// 2.2. BookNow
export function BookNowIconAndTextButton(
    props: GenericButtonProps
): JSX.Element {
    const { componentAttrs, size } = props;
    const { t } = useTranslation();
    return (
        <Button {...componentAttrs}
            aria-label="BookNow"
            size={size}
            color="primary"
            variant="contained"
            startIcon={<EventAvailableIcon />}
        >
            {t("BookNow")}
        </Button>
    );
}

// 3.1. Join
export function JoinIconAndTextRouteButton(
    props: RouteButtonProps
): JSX.Element {
    const { pageUrl, routeParams, queryStringParams, size } = props;
    const { t } = useTranslation();
    const url = buildFullUrl(pageUrl, routeParams, queryStringParams, null);
    return (
        <Button component={RouterLink} to={url}
            aria-label="Join"
            size={size}
            color="primary"
            variant="contained"
            startIcon={<PersonAddIcon />}
        >
            {t("Join")}
        </Button>
    );
}

// 3.2. Join
export function JoinIconAndTextButton(
    props: GenericButtonProps
): JSX.Element {
    const { componentAttrs, size } = props;
    const { t } = useTranslation();
    return (
        <Button {...componentAttrs}
            aria-label="Join"
            size={size}
            color="primary"
            variant="contained"
            startIcon={<PersonAddIcon />}
        >
            {t("Join")}
        </Button>
    );
}

// 4.1. JoinGroup
export function JoinGroupIconAndTextRouteButton(
    props: RouteButtonProps
): JSX.Element {
    const { pageUrl, routeParams, queryStringParams, size } = props;
    const { t } = useTranslation();
    const url = buildFullUrl(pageUrl, routeParams, queryStringParams, null);
    return (
        <Button component={RouterLink} to={url}
            aria-label="JoinGroup"
            size={size}
            color="primary"
            variant="contained"
            startIcon={<GroupAddIcon />}
        >
            {t("JoinGroup")}
        </Button>
    );
}

// 4.2. JoinGroup
export function JoinGroupIconAndTextButton(
    props: GenericButtonProps
): JSX.Element {
    const { componentAttrs, size } = props;
    const { t } = useTranslation();
    return (
        <Button {...componentAttrs}
            aria-label="JoinGroup"
            size={size}
            color="primary"
            variant="contained"
            startIcon={<GroupAddIcon />}
        >
            {t("JoinGroup")}
        </Button>
    );
}

// 5.1. Checkout
export function CheckoutIconAndTextRouteButton(
    props: RouteButtonProps
): JSX.Element {
    const { pageUrl, routeParams, queryStringParams, size } = props;
    const { t } = useTranslation();
    const url = buildFullUrl(pageUrl, routeParams, queryStringParams, null);
    return (
        <Button component={RouterLink} to={url}
            aria-label="Checkout"
            size={size}
            color="primary"
            variant="contained"
            startIcon={<ShoppingCartCheckoutIcon />}
        >
            {t("Checkout")}
        </Button>
    );
}

// 5.2. Checkout
export function CheckoutIconAndTextButton(
    props: GenericButtonProps
): JSX.Element {
    const { componentAttrs, size } = props;
    const { t } = useTranslation();
    return (
        <Button {...componentAttrs}
            aria-label="Checkout"
            size={size}
            color="primary"
            variant="contained"
            startIcon={<ShoppingCartCheckoutIcon />}
        >
            {t("Checkout")}
        </Button>
    );
}

