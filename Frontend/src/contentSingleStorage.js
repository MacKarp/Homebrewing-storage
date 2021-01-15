import * as React from 'react';
//import ReactDOM from 'react-dom';

class ContentSingleStorage extends React.Component  {
  constructor(props){
    super(props);
    this.state = {
        items: [],
        storageId: props.storageId
      };
    }

      componentDidMount() {
        fetch("http://localhost:8080/api/Expire")
          .then(res => res.json())
         // .then(json => this.setState({ items: json}));
          .then(json => this.setState({ items: json.filter(exp => exp.idStorage === this.state.storageId)}))
      }

      itemsToSingleItem = item => {
        const expireId = item.expireId;
        const idItem = item.idItem;
        const expirationDate = item.expirationDate;
        return <Item key={expireId} idItem={idItem} expirationDate={expirationDate}/>;
      };



    render(){

        return(
            <div id="showAllItems">
                {this.state.items.map(this.itemsToSingleItem)}
            </div>
        )
    }
}


  const Item = ({expireId,idItem,expirationDate}) =>{

    //{idUser}
    return (
      <div className="singleItem">
        {expireId})<br />{idItem}<br />{expirationDate}  
      </div>
  );
    };


export default ContentSingleStorage;