import * as React from 'react';
//import ReactDOM from 'react-dom';

class AddExpirationItem extends React.Component  {
  constructor(props){
    super(props);
    this.state = {
        token: props.token,
        postId: null,
        itemID: props.itemID,
        expDate: props.expDate,
        idStorage:props.idStorage,
        user:props.user
      };
    }



   componentDidMount() {
        // Simple POST request with a JSON body using fetch
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json-patch+json', 'Authorization': this.state.token},
            body: JSON.stringify({ userId: this.state.user, idStorage: this.state.idStorage, idItem: this.state.itemID, expirationDate: this.state.expDate })
        };
        fetch('http://localhost:8080/api/Expire', requestOptions)
            .then(res => res.json())
            .then(data => this.setState({ postId: data.id }))
    }




    render(){

        return(
            <div id="addStorage">
                   <b>Dodano!</b>
            </div>
        )
    }
}




export default AddExpirationItem;