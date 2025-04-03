import { CrudViewContainers } from "src/shared/viewModels/CrudViewContainers";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import CloseIcon from '@mui/icons-material/Close';

export function getDoneButtonIconAndText(crudViewContainer: CrudViewContainers)
    : {icon: JSX.Element, text: string} {
    if(crudViewContainer === CrudViewContainers.StandaloneView) {
        return {icon: <ArrowBackIcon />, text: "Back"};
    }

    return {icon: <CloseIcon />, text: "Done"};
}
