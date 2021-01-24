import * as React from 'react';
//import ReactDOM from 'react-dom';

class ItemName extends React.Component  {
  constructor(props){
    super(props);
    this.state = {
        token: props.token,
        idItem: props.idItem,
        user: props.user,
        item: null
      };
    }

    componentDidMount() {
      const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json-patch+json', 'Authorization': this.state.token},
    };
        fetch('http://localhost:8080/api/Item/'+this.state.idItem,requestOptions)
          .then(res => res.json()).then(json => this.setState({ item: json.itemName}))

      }


    render(){

        return(
          this.state.item
        )
    }

}
      export default ItemName