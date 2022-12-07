import { createRoot } from "react-dom/client";
import * as React from "react";

import "./index.css";
import App from "./App";

//import reportWebVitals from './reportWebVitals';

const root = createRoot(document.getElementById("root")!);
root.render(
    //TODO disabled this because it makes the Callback effect fire twice and breaks oauth
    // https://stackoverflow.com/questions/61254372/my-react-component-is-rendering-twice-because-of-strict-mode/61897567#61897567
    //<React.StrictMode>
    <App />
    //</React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
//reportWebVitals();
