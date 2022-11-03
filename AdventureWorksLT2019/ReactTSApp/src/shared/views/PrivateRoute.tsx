import { useSelector } from 'react-redux';
import {
    Navigate,
    RouteProps,
    useLocation,
} from 'react-router-dom';
import { RootState } from 'src/store/CombinedReducers';

export default function PrivateRoute({ children }: RouteProps): JSX.Element {
    const auth = useSelector((state: RootState) => state.auth);
    const location = useLocation();
    const from = location.pathname;

    if (auth.isAuthenticated) {
        return (<>{children}</>);
    }
    return <Navigate to={"/autologin?from=" + from} />;
}