import * as React from 'react';
//import ReactDOM from 'react-dom';
import AppLoggedIn from './appLoggedIn.js';
import AppLoggedOff from './appLoggedOff.js';

//SHOWSTATES: 
// 1. MAIN APP

 class App extends React.Component  {
   constructor(){
   super();
   this.handleLoginState=this.handleLoginState.bind(this);
    this.state={
      logged: false,
      showState: 1 //1. MAIN APP
    }
   }
   handleLoginState(){
      this.setState({logged:!this.state.logged})
   }
   isLogged(){
     return this.state.logged;
   }
   
   renderSwitch(param) {
    switch(param) {
      case 1:
        if(this.isLogged()) {
          return ( 
            <div>Zalogowano
            <button onClick={this.handleLoginState}>Zmień login state</button>
            <AppLoggedIn/>
            </div>
          );  
            }
            else {
              return ( 
                <div>Niezalogowano
            <button onClick={this.handleLoginState}>Zmień login state</button>  
            <AppLoggedOff/>
                </div>
              );  
      }
      case 2:
        return  (<div>Opcja nr 2</div>)
      default:
        return 'BŁĄD';
    }
  }
   


    render(){
      return(
      this.renderSwitch(this.state.showState)  
      )    
    }
  }
  export default App;