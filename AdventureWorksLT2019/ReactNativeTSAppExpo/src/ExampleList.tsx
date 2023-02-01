import React from "react";
import HelloWorld1 from "./1.HelloWorld1";
import IndexPage from "./views/BuildVersion/IndexPage"
export const EXAMPLE_LIST = [
  {
    name: "Hello World 1",
    level: 1,
    component: <HelloWorld1 />,
  },
  {
    name: "Build Version Index Page",
    level: 1,
    component: <IndexPage />,
  }
];
