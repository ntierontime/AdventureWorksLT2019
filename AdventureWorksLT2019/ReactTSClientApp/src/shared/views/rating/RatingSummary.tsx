import { Grid, Rating, Slider, Stack, Typography } from "@mui/material";

import { useTranslation } from "react-i18next";

import { ILabelValuePair } from "src/shared/dataModels/ILabelValuePair";
import { xs1sm1md3lg3xl3GridItem } from "../ResponsiveGridItem";

export const calcRatingAverage = (ratingCount?: number, ratingTotal?: number) => {
    return !!!ratingCount || !!!ratingTotal || ratingCount === 0 ? 0 : ratingTotal / ratingCount;
}

export interface RatingSummaryProps {
    ratingCount: number;
    ratingAverage: number;
    ratingCountList: ILabelValuePair[];
    ratingAverageList: ILabelValuePair[];
}

export const RatingSummary = (props: RatingSummaryProps): JSX.Element => {
    const { t } = useTranslation();

    const {
        ratingCount, // minutesStep not working when use up/down arrow keys on keyboard
        ratingAverage = 0,
        ratingCountList,
        ratingAverageList,
    } = props;
    const ratingCountList_Max = Math.max(...ratingCountList.map(item => item.value));

    return (
        <Grid container>
            <Grid item {...xs1sm1md3lg3xl3GridItem}>
                <Stack direction="column" spacing={0} width={{xs: '100%', md: '70%'}} height="100%" justifyContent="center" alignItems="center">
                    <Typography gutterBottom variant="h4" component="div" padding={0} margin={0}>
                        {ratingAverage}
                    </Typography>
                    <Rating name="half-rating" value={ratingAverage} precision={0.1} readOnly />
                    <Typography gutterBottom variant="subtitle1" component="div" padding={0} margin={0}>
                        ({ratingCount})
                    </Typography>
                </Stack>
            </Grid>
            {!!ratingCountList && <Grid item {...xs1sm1md3lg3xl3GridItem}>
                {ratingCountList.map((item, index) =>
                    <Stack key={item.label} spacing={2} direction="row" sx={{ mb: 1 }} width={{xs: '100%', md: '70%'}} justifyContent="center" alignItems="center">
                        <Typography gutterBottom variant="subtitle1" component="div" padding={0} margin={0}>
                            {item.label}
                        </Typography>
                        <Slider aria-label={item.label} value={item.value} disabled max={ratingCountList_Max} sx={{width: "50%"}} />
                        <Typography gutterBottom variant="subtitle1" component="div" padding={0} margin={0}>
                            ({item.value})
                        </Typography>
                    </Stack>
                )}
            </Grid>}
            {!!ratingAverageList && <Grid item {...xs1sm1md3lg3xl3GridItem}>
                {ratingAverageList.map((item, index) =>
                    <Stack key={item.label} spacing={2} direction="row" sx={{ mb: 1 }} width={{xs: '100%', md: '70%'}} justifyContent="end" alignItems="center">
                        <Typography gutterBottom variant="subtitle1" component="div" padding={0} margin={0} width='50%'>
                            {item.label}
                        </Typography>
                        <Rating name="rating" value={item.value} precision={0.1} readOnly sx={{ width: '50%'}}/>
                    </Stack>
                )}
            </Grid>}
            {/* <Stack direction={{sx: "column", md: "row"}} justifyContent="space-around">
        <Stack direction="column" spacing={0} justifyContent="center" alignItems="center">
                <Typography gutterBottom variant="h4" component="div" padding={0} margin={0}>
                    {ratingAverage}
                </Typography>
                <Rating name="half-rating" value={ratingAverage} precision={0.1} readOnly />
                <Typography gutterBottom variant="subtitle1" component="div" padding={0} margin={0}>
                    ({ratingCount})
                </Typography>
            </Stack>
            <Stack direction="column" spacing={0} width='100' justifyContent="center" alignItems="center">
                {!!ratingCountList && ratingCountList.map((item, index) =>
                    <Stack key={item.label} spacing={2} direction="row" sx={{ mb: 1, width: '100%' }} alignItems="center">
                        <Typography gutterBottom variant="subtitle1" component="div" padding={0} margin={0}>
                            {item.label}
                        </Typography>
                        <Slider aria-label={item.label} value={item.value} disabled max={ratingCountList_Max} />
                        <Typography gutterBottom variant="subtitle1" component="div" padding={0} margin={0}>
                            ({item.value})
                        </Typography>
                    </Stack>
                )}
            </Stack>
            <Stack direction="column" spacing={0} justifyContent="center" alignItems="center">
                {!!ratingAverageList && ratingAverageList.map((item, index) =>
                    <Stack key={item.label} spacing={2} direction="row" sx={{ mb: 1, width: '100vw' }} justifyContent="space-between" alignItems="center">
                        <Typography gutterBottom variant="subtitle1" component="div" padding={0} margin={0}>
                            {item.label}
                        </Typography>
                        <Rating name="half-rating" value={item.value} precision={0.1} readOnly />
                    </Stack>
                )}
            </Stack> 
        </Stack>*/}
        </Grid>
    );
}
