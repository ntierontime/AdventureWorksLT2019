import { Badge, Grid } from "@mui/material";
import {
    EmailIcon,
    EmailShareButton,
    FacebookIcon,
    FacebookMessengerIcon,
    FacebookMessengerShareButton,
    FacebookShareButton,
    FacebookShareCount,
    GabIcon,
    GabShareButton,
    HatenaIcon,
    HatenaShareButton,
    HatenaShareCount,
    InstapaperIcon,
    InstapaperShareButton,
    LineIcon,
    LineShareButton,
    LinkedinIcon,
    LinkedinShareButton,
    LivejournalIcon,
    LivejournalShareButton,
    MailruIcon,
    MailruShareButton,
    OKIcon,
    OKShareButton,
    OKShareCount,
    PinterestIcon,
    PinterestShareButton,
    PinterestShareCount,
    PocketIcon,
    PocketShareButton,
    RedditIcon,
    RedditShareButton,
    RedditShareCount,
    TelegramIcon,
    TelegramShareButton,
    TumblrIcon,
    TumblrShareButton,
    TumblrShareCount,
    TwitterShareButton,
    ViberIcon,
    ViberShareButton,
    VKIcon,
    VKShareButton,
    VKShareCount,
    WeiboIcon,
    WeiboShareButton,
    WhatsappIcon,
    WhatsappShareButton,
    WorkplaceIcon,
    WorkplaceShareButton,
    XIcon,
} from "react-share";

import { useTranslation } from "react-i18next";
import { SocialShareProps } from "./SocialShareProps";

const gridItemSX = { paddingLeft: 1, paddingRight: 1 };

export default function SocialShare(props: SocialShareProps): JSX.Element {
    const { shareUrl, title, withCount, exampleImage } = props;
    const { t } = useTranslation();

    return (
        <Grid container alignContent="start" alignItems="start">
            <Grid item sx={gridItemSX}>
                <FacebookShareButton title={t("Facebook")} url={shareUrl}>
                    <FacebookIcon size={32} round />
                </FacebookShareButton>
                {/* <Badge
                    badgeContent={
                        <FacebookShareCount url={shareUrl}>
                            {(count) => count}
                        </FacebookShareCount>
                    }
                    color="primary"
                    >
                    <FacebookShareButton title={t("Facebook")} url={shareUrl}>
                        <FacebookIcon size={32} round />
                    </FacebookShareButton>
                </Badge> */}
            </Grid>

            <Grid item sx={gridItemSX}>
                <FacebookMessengerShareButton
                    url={shareUrl}
                    appId="521270401588372"
                >
                    <FacebookMessengerIcon size={32} round />
                </FacebookMessengerShareButton>
            </Grid>

            <Grid item sx={gridItemSX}>
                <TwitterShareButton url={shareUrl} title={title}>
                    <XIcon size={32} round />
                </TwitterShareButton>
            </Grid>

            <Grid item sx={gridItemSX}>
                <TelegramShareButton url={shareUrl} title={title}>
                    <TelegramIcon size={32} round />
                </TelegramShareButton>
            </Grid>

            <Grid item sx={gridItemSX}>
                <WhatsappShareButton
                    url={shareUrl}
                    title={title}
                    separator=":: "
                >
                    <WhatsappIcon size={32} round />
                </WhatsappShareButton>
            </Grid>

            <Grid item sx={gridItemSX}>
                <LinkedinShareButton url={shareUrl}>
                    <LinkedinIcon size={32} round />
                </LinkedinShareButton>
            </Grid>

            <Grid item sx={gridItemSX}>
                <PinterestShareButton
                    url={String(window.location)}
                    media={`${String(window.location)}/${exampleImage}`}
                >
                    <PinterestIcon size={32} round />
                </PinterestShareButton>
                {/* <Badge
                    badgeContent={<PinterestShareCount url={shareUrl} />}
                    color="primary"
                >
                    <PinterestShareButton
                    url={String(window.location)}
                    media={`${String(window.location)}/${exampleImage}`}
                    >
                        <PinterestIcon size={32} round />
                    </PinterestShareButton>
                </Badge> */}
            </Grid>
 
            {/* <Grid item sx={gridItemSX}>
                <VKShareButton
                    url={shareUrl}
                    image={`${String(window.location)}/${exampleImage}`}
                >
                    <VKIcon size={32} round />
                </VKShareButton>
                <Badge
                    badgeContent={<VKShareCount url={shareUrl} />}
                    color="primary"
                >
                    <VKShareButton
                    url={shareUrl}
                    image={`${String(window.location)}/${exampleImage}`}
                    >
                        <VKIcon size={32} round />
                    </VKShareButton>
                </Badge>
            </Grid>

            <Grid item sx={gridItemSX}>
                <OKShareButton
                    url={shareUrl}
                    image={`${String(window.location)}/${exampleImage}`}
                >
                    <OKIcon size={32} round />
                </OKShareButton>
                <Badge badgeContent={<OKShareCount url={shareUrl} />} color="primary">
                    <OKShareButton
                    url={shareUrl}
                    image={`${String(window.location)}/${exampleImage}`}
                    >
                        <OKIcon size={32} round />
                    </OKShareButton>
                </Badge>
            </Grid> */}

            <Grid item sx={gridItemSX}>
                <RedditShareButton
                    url={shareUrl}
                    title={title}
                    windowWidth={660}
                    windowHeight={460}
                >
                    <RedditIcon size={32} round />
                </RedditShareButton>

                {/* <Badge badgeContent={<RedditShareCount url={shareUrl} />} color="primary">
                    <RedditShareButton
                    url={shareUrl}
                    title={title}
                    windowWidth={660}
                    windowHeight={460}
                    >
                        <RedditIcon size={32} round />
                    </RedditShareButton>
                </Badge> */}
            </Grid>

            {/* <Grid item sx={gridItemSX}>
                <GabShareButton
                    url={shareUrl}
                    title={title}
                    windowWidth={660}
                    windowHeight={640}
                >
                    <GabIcon size={32} round />
                </GabShareButton>
            </Grid>

            <Grid item sx={gridItemSX}>
                <TumblrShareButton url={shareUrl} title={title}>
                    <TumblrIcon size={32} round />
                </TumblrShareButton>

                <Badge badgeContent={<TumblrShareCount url={shareUrl} />} color="primary">
                    <TumblrShareButton url={shareUrl} title={title}>
                        <TumblrIcon size={32} round />
                    </TumblrShareButton>
                </Badge>
            </Grid>

            <Grid item sx={gridItemSX}>
                <LivejournalShareButton
                    url={shareUrl}
                    title={title}
                    description={shareUrl}
                >
                    <LivejournalIcon size={32} round />
                </LivejournalShareButton>
            </Grid>

            <Grid item sx={gridItemSX}>
                <MailruShareButton url={shareUrl} title={title}>
                    <MailruIcon size={32} round />
                </MailruShareButton>
            </Grid> */}

            <Grid item sx={gridItemSX}>
                <EmailShareButton url={shareUrl} subject={title} body="body">
                    <EmailIcon size={32} round />
                </EmailShareButton>
            </Grid>

            {/* <Grid item sx={gridItemSX}>
                <ViberShareButton url={shareUrl} title={title}>
                    <ViberIcon size={32} round />
                </ViberShareButton>
            </Grid>

            <Grid item sx={gridItemSX}>
                <WorkplaceShareButton url={shareUrl} quote={title}>
                    <WorkplaceIcon size={32} round />
                </WorkplaceShareButton>
            </Grid> */}

            <Grid item sx={gridItemSX}>
                <LineShareButton url={shareUrl} title={title}>
                    <LineIcon size={32} round />
                </LineShareButton>
            </Grid>

            <Grid item sx={gridItemSX}>
                <WeiboShareButton
                    url={shareUrl}
                    title={title}
                    image={`${String(window.location)}/${exampleImage}`}
                >
                    <WeiboIcon size={32} round />
                </WeiboShareButton>
            </Grid>

            <Grid item sx={gridItemSX}>
                <PocketShareButton url={shareUrl} title={title}>
                    <PocketIcon size={32} round />
                </PocketShareButton>
            </Grid>

            <Grid item sx={gridItemSX}>
                <InstapaperShareButton url={shareUrl} title={title}>
                    <InstapaperIcon size={32} round />
                </InstapaperShareButton>
            </Grid>

            {/* <Grid item sx={gridItemSX}>
                <HatenaShareButton
                    url={shareUrl}
                    title={title}
                    windowWidth={660}
                    windowHeight={460}
                >
                    <HatenaIcon size={32} round />
                </HatenaShareButton>

                <Badge badgeContent={<HatenaShareCount url={shareUrl} />} color="primary">
                    <HatenaShareButton
                    url={shareUrl}
                    title={title}
                    windowWidth={660}
                    windowHeight={460}
                    >
                        <HatenaIcon size={32} round />
                    </HatenaShareButton>
                </Badge>
            </Grid> */}
        </Grid>
    );
}
