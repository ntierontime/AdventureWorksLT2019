import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { LinearProgress } from "@mui/material";
import dayjs from "dayjs";

import { RootState } from "src/store/CombinedReducers";
import { AppDispatch } from "src/store/Store";
import { setIsAuthenticated } from "src/slices/msIdentityFrameworkSlice";

export default function AutoLogout() {
    const dispatch = useDispatch<AppDispatch>();
    const navigate = useNavigate();
    const auth = useSelector((state: RootState) => state.auth);

    useEffect(() => {
        if(auth.isAuthenticated) {
            dispatch(setIsAuthenticated(false));
        }
        else {
            navigate('/login');
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [auth.isAuthenticated]);

    return (<LinearProgress />)
}

