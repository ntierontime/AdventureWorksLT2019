import { Container, Divider, Grid, Link, Paper, Stack, styled } from "@mui/material";
import Typography from "@mui/material/Typography";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

export default function AppFooter() {
    const { t } = useTranslation();
    const navigate = useNavigate();

    return (
        <Container maxWidth="md"  >
            <Paper >
                <Divider sx={{ marginY: 1 }} />
                <Grid container spacing={0} >
                    <Grid item xs={12} md={3}>
                        <Stack direction='column'
                            justifyContent="flex-start"
                            alignItems="flex-start">
                            <Typography component='h2' variant="h6">{t("DownloadApp")}</Typography>
                            <Divider sx={{ marginY: 1 }} />
                            <Link underline="hover" component="button"
                                onClick={() => {
                                    navigate("#");
                                }}>
                                {t("AppleAppStore")}
                            </Link>
                            <Link underline="hover" component="button"
                                onClick={() => {
                                    navigate("#");
                                }}>
                                {t("GoogleAppStore")}
                            </Link>
                        </Stack>
                    </Grid>
                    <Grid item xs={12} md={3}>
                        <Stack direction='column'
                            justifyContent="flex-start"
                            alignItems="flex-start">
                            <Typography component='h2' variant="h6">{t("Company")}</Typography>
                            <Divider sx={{ marginY: 1 }} />
                            <Link underline="hover" component="button"
                                onClick={() => {
                                    navigate("/aboutus");
                                }}>
                                {t("AboutUs")}
                            </Link>
                            <Link underline="hover" component="button"
                                onClick={() => {
                                    navigate("/contactus");
                                }}>
                                {t("ContactUs")}
                            </Link>
                        </Stack>
                    </Grid>
                    <Grid item xs={12} md={3}>
                        <Stack direction='column'
                            justifyContent="flex-start"
                            alignItems="flex-start">
                            <Typography component='h2' variant="h6">{t("Discover")}</Typography>
                            <Divider sx={{ marginY: 1 }} />
                            <Link underline="hover" component="button"
                                onClick={() => {
                                    navigate("/useragreement");
                                }}>
                                {t("UserAgreement")}
                            </Link>
                            <Link underline="hover" component="button"
                                onClick={() => {
                                    navigate("/privacypolicy");
                                }}>
                                {t("PrivacyPolicy")}
                            </Link>
                        </Stack>
                    </Grid>
                    <Grid item xs={12} md={3}>
                        <Stack direction='column'
                            justifyContent="flex-start"
                            alignItems="flex-start">
                            <Typography component='h2' variant="h6">{t("DownloadApp")}</Typography>
                            <Divider sx={{ marginY: 1 }} />
                            <Typography>{t("AppleAppStore")}</Typography>
                            <Typography>{t("GoogleAppStore")}</Typography>
                        </Stack>
                    </Grid>
                </Grid>
            </Paper>
        </Container>
    );
}