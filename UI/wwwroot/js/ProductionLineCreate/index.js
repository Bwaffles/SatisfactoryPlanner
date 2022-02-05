import React from 'react';
import ReactDOM from 'react-dom';
import ReactDOMServer from 'react-dom/server';
import '../../lib/semantic/dist/semantic.css';
import ProductionLineCreate from './ProductionLineCreate.jsx';

global.React = React;
global.ReactDOM = ReactDOM;
global.ReactDOMServer = ReactDOMServer;

global.Components = { ProductionLineCreate };