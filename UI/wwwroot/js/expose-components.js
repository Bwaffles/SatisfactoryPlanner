import React from 'react';
import ReactDOM from 'react-dom';
import ReactDOMServer from 'react-dom/server';

import RootComponent from './NewProductionLine.jsx';

import '../lib/semantic/dist/semantic.css'

global.React = React;
global.ReactDOM = ReactDOM;
global.ReactDOMServer = ReactDOMServer;

global.Components = { RootComponent };