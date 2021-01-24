import * as React from 'react';
//import ReactDOM from 'react-dom';
import imglogo from './images/logo.png';


class Logo extends React.Component  {
      render(){
          return(
            <div id="logo">
                <img src={imglogo} alt="logo"/>
            </div>
          )
      }
    }
      export default Logo; 