import * as React from 'react';
import ShowAllStorages from './showAllStorages.js';
//import ReactDOM from 'react-dom';
import AddStorage from './addStorage.js';

class AppLoggedIn extends React.Component  {
    constructor(props){
        super(props);
        this.handleStateSet = this.handleStateSet.bind(this);
        this.handleInputState = this.handleInputState.bind(this); // tutaj bind!
        this.state={
            user: props.userId,
            showState: 1,
            rand: Math.random(),
            token: props.token,
    }
}

handleStateSet = (e) =>{
    this.setState({showState: parseInt(e.target.value,10),rand: Math.random()})
}
belka() {
    return ( <div id="loggedBelka"><button className="classicButton" onClick={this.handleStateSet} value={1}>Pokaż Magazyny</button><button className="classicButton" onClick={this.handleStateSet} value={2}>Nowy Magazyn</button></div>)
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
          <div id="showCenter" className={this.state.user}>
              <ShowAllStorages key={this.state.rand} user={this.state.user} token={this.state.token}/>
          </div>
          <div id="loggedBottom"></div>
      </div>)
    case 2:
        return(
            <div>{this.belka()}
           Dodaj magazyn: <input onInput={this.handleInputState} /> <button className="classicButton" onClick={this.handleStateSet} value={3}>Dodaj</button></div>)
    case 3:
            return(
                <div>{this.belka()}
                {console.log(this.state.user)}
               <AddStorage  token={this.state.token} idUser={this.state.user} nameStorage={this.state.newStorage}/></div>)
           
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