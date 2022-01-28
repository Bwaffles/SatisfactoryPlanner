import React from 'react';

// Prompt to start creating a new pod item
export class AddPodItem extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            isToggleOn: true
        };

        // This binding is necessary to make `this` work in the callback
        this.addPod = this.addPod.bind(this);
    }

    addPod() {
        this.props.onAddNewPod();
    }

    render() {
        return (
            <div className="ui center aligned blue very padded text raised container segment">
                <div className="ui icon header">
                    Add New Pod
                </div>
                <div className="inline">
                    <button className="ui primary button" onClick={this.addPod}>
                        Add <i className="icon add"></i>
                    </button>
                </div>
            </div>
        );
    }
}