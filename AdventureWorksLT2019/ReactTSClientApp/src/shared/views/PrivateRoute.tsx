import { useSelector } from 'react-redux';
import {
    Navigate,
    RouteProps,
    useLocation,
} from 'react-router-dom';
import { RootState } from 'src/store/CombinedReducers';

export default function PrivateRoute({ children }: RouteProps): JSX.Element {
    const location = useLocation();
    const auth = useSelector((state: RootState) => state.auth);

    if (auth.isAuthenticated) {
        return (<>{children}</>);
    }

    const from = location.pathname + location.search;
    return <Navigate to={"/autologin?from=" + encodeURIComponent(from)} />;
}