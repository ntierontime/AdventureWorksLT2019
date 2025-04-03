import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { LinearProgress } from "@mui/material";
import dayjs from "dayjs";

import { RootState } from "src/store/CombinedReducers";
import { AppDispatch } from "src/store/Store";
import { setIsAuthenticated } from "src/slices/msIdentityFrameworkSlice";

export default function AutoLogIn() {
    const dispatch = useDispatch<AppDispatch>();
    const navigate = useNavigate();
    const queryParams = new URLSearchParams(window.location.search)
    const from = queryParams.get("from");
    const auth = useSelector((state: RootState) => state.auth);

    useEffect(() => {
        if (auth.isAuthenticated && !!!auth.expiringAt && dayjs(auth.expiringAt) > dayjs()) {
            setTimeout(() => { navigate(from); }, 1000);
        }
        else {
            setIsAuthenticated(false);
            navigate("/login?from=" + from);
        }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (<LinearProgress />)
}

