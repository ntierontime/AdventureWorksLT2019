import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { LinearProgress } from "@mui/material";
import { AppDispatch } from "src/store/Store";
import { setIsAuthenticated } from "src/slices/authenticationSlice";
import { useNavigate } from "react-router-dom";

export default function AutoLogIn() {
    const dispatch = useDispatch<AppDispatch>();
    const navigate = useNavigate();
    const queryParams = new URLSearchParams(window.location.search)
    const from = queryParams.get("from");

    useEffect(() => {
        const loggedInUser = localStorage.getItem("user");
        if (loggedInUser) {
            JSON.parse(loggedInUser);
            dispatch(setIsAuthenticated());
            setTimeout(() => { navigate(from); }, 1000);
        }
        else {
            navigate("/account/login?from=" + from);
        }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (<LinearProgress />)
}