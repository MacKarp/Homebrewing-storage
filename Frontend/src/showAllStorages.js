import * as React from 'react';
//import ReactDOM from 'react-dom';
import ContentSingleStorage from './contentSingleStorage.js';
import DelStorage from './delStorage.js';
import AddItemToStorage from './addItemToStorage.js';

class ShowAllStorages extends React.Component  {
  constructor(props){
    super(props);
    this.openSingleStorage = this.openSingleStorage.bind(this);
    this.state = {
        storages: [],
        user: props.user,
        showState: 1,
        storId: 0,
        token: 'Bearer '+props.token
      };
    }

      componentDidMount() {

        const requestOptions = {
          method: 'GET',
          headers: { 'Content-Type': 'accept: text/plain', 'Authorization' : this.state.token}
      };

       fetch("http://localhost:8080/api/Storage",requestOptions)
          .then(res => res.json())
         // .then(json => this.setState({ storages: json}))//niefiltrowane
        .then(json => this.setState({ storages: json.filter(user => user.idUser === this.state.user)}))//  FILTROWANE USEREM
      }
      handleStateSet = (e) =>{
        if(e.target.value==='4') { if(window.confirm("Czy na pewno usunąć?")){this.setState({showState: parseInt(e.target.value,10)})}; }
        else {this.setState({showState: parseInt(e.target.value,10)})}
    }
    breakline(){
      return "<br />"
    }
      storagesToSingleStorage = storage => {

        const storageId = storage.storageId;
       // const idUser = storage.idUser;
        const storageName = storage.storageName;
        //this.setState({showHelper: 2})
        return <button className="singleStorage" key={storageId} value={storageId} onClick={this.openSingleStorage}>{storageName}</button>


      };
      openSingleStorage = (e) =>{
        this.setState({storId: parseInt(e.target.value,10),showState:2})
      }

      renderSwitch(param) {
    switch(param) {
      case 1:
          return(<div id="showAllStorages">
          {this.state.storages.map(this.storagesToSingleStorage)}
      </div>)
    case 2:
        return(<div><ContentSingleStorage token={this.state.token} user={this.state.user} key={this.state.storId} storageId={this.state.storId}/><center><button  className="classicButton" value={3} onClick={this.handleStateSet}>Dodaj</button><button className="classicButton" value={4} onClick={this.handleStateSet}>Usuń</button><button value={1} className="classicButton" onClick={this.handleStateSet}>Powrót</button></center></div>)
    case 3:
        return (<div id="dodajDoBibl"><span>Dodaj przedmiot do magazynku: <AddItemToStorage token={this.state.token} user={this.state.user} storageId={this.state.storId}/></span></div>)
    case 4:
        return (<div><DelStorage  token={this.state.token} storageId={this.state.storId} idUser={this.state.user}/></div>)
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



export default ShowAllStorages;