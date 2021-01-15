import * as React from 'react';
import ReactDOM from 'react-dom';
import App from './app.js';
import './app.css';






class Index extends React.Component  {
    render(){
    return ( <div id="mainApp"><App /></div>
    );  
    }
  }
  


  ReactDOM.render(<Index />, document.getElementById("root")); 