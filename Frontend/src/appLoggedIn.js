import * as React from 'react';
import ShowAllStorages from './showAllStorages.js';
//import ReactDOM from 'react-dom';
import AddStorage from './addStorage.js';

class AppLoggedIn extends React.Component  {
    constructor(){
        super();
        this.handleStateSet = this.handleStateSet.bind(this);
        this.handleInputState = this.handleInputState.bind(this); // tutaj bind!
        this.state={
            user: '68590398-f1f8-45d7-b7e7-1e8e128277fb',
            showState: 1,
            rand: Math.random(),
            newStorage: null
    }
}

handleStateSet = (e) =>{
    this.setState({showState: parseInt(e.target.value,10),rand: Math.random()})
}
belka() {
    return ( <div id="loggedBelka">belka dla zalogowanych<button onClick={this.handleStateSet} value={1}>BIBLIOTECZKI ( AppState na 1</button><button onClick={this.handleStateSet} value={2}>DODAJ STORAGE ( AppState na 2</button></div>)
}

handleInputState = (e) => {
    this.setState({newStorage:e.currentTarget.value })
}

renderSwitch(param) {
    switch(param) {
      case 0: return(<div>Ładuję</div>)
      case 1:
          return(<div id="loggedMain">
              {this.belka()}
          <div id="showCenter">
              <ShowAllStorages key={this.state.rand} user={this.state.user}/>
          </div>
          <div id="loggedBottom">Tu będzie stopka</div>
      </div>)
    case 2:
        return(
            <div>{this.belka()}
           Dodaj biblioteczkę <input onInput={this.handleInputState} /> <button onClick={this.handleStateSet} value={3}>Dodaj</button></div>)
    case 3:
            return(
                <div>{this.belka()}
               <AddStorage idUser={this.state.user} nameStorage={this.state.newStorage}/></div>)
           
    default:
        return(<div>Błąd</div>)
    }
}


    render(){
        return(
        this.renderSwitch(this.state.showState)  
        )    
      }


}
export default AppLoggedIn;