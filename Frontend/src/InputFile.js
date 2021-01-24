import React from 'react';


class InputFile extends React.Component{
  
  render(){
  return (
    <div className="inputFile">
        <input
        className='input'
        type={this.props.type}
        placeholder={this.props.placeholder}
        value={this.props.value}
        onChange={(e) => this.props.onChange(e.target.value)}
        />

    </div>
  );
}
}
export default InputFile;
