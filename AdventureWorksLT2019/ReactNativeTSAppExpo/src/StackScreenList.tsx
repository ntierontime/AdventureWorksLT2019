import HelloWorld1 from "./1.HelloWorld1";
import { ViewItemTemplates } from "./shared/viewModels/ViewItemTemplates";
import CreatePage from "./views/BuildVersion/CreatePage";
import IndexPage from "./views/BuildVersion/IndexPage"
import ItemPage from "./views/BuildVersion/ItemPage";

export const StackScreenList = [
  {
    name: "HelloWorld1",
    component: <HelloWorld1 />,
  },
  {
    name: "BuildVersionIndexPage",
    component: <IndexPage />,
  }
  ,
  {
    name: "BuildVersionCreatePage",
    component: <CreatePage /> as JSX.Element,
  }
  ,
  {
    name: "BuildVersionDetailsPage",
    component: <ItemPage viewItemTemplate={ViewItemTemplates.Details} /> as JSX.Element,
  }
  ,
  {
    name: "BuildVersionEditPage",
    component: <ItemPage viewItemTemplate={ViewItemTemplates.Edit} /> as JSX.Element,
  }
] as {name: string, component: JSX.Element}[];
