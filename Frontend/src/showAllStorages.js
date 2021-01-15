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
        storId: 0
      };
    }

      componentDidMount() {
        fetch("http://localhost:8080/api/Storage")
          .then(res => res.json())
          .then(json => this.setState({ storages: json}))//niefiltrowane
        // FILTROWANE USEREM .then(json => this.setState({ storages: json.filter(user => user.idUser === this.state.user)}))
      }
      handleStateSet = (e) =>{
        if(e.target.value==='4') { if(window.confirm("Czy na pewno usunąć?")){this.setState({showState: parseInt(e.target.value,10)})}; }
        else {this.setState({showState: parseInt(e.target.value,10)})}
    }
      storagesToSingleStorage = storage => {
        const storageId = storage.storageId;
       // const idUser = storage.idUser;
        const storageName = storage.storageName;
        return <button className="singleStorage" key={storageId} value={storageId} onClick={this.openSingleStorage}>{storageId} - {storageName}</button>;
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
        return(<div><ContentSingleStorage key={this.state.storId} storageId={this.state.storId}/><button value={3} onClick={this.handleStateSet}>Dodaj</button><button value={4} onClick={this.handleStateSet}>Usuń</button><button value={1} onClick={this.handleStateSet}>Powrót</button></div>)
    case 3:
        return (<div>Dodaj przedmiot do biblioteki: <AddItemToStorage storageId={this.state.storId}/></div>)
    case 4:
        return (<div><DelStorage storageId={this.state.storId} idUser={this.state.user}/></div>)
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