import React from 'react';

import { AddPodItem } from './AddPodItem.jsx';
import { PodItemCreator } from './PodItemCreator.jsx';

export default class NewProductionLine extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            items: [
            ]
        };

        this.startCreatingPod = this.startCreatingPod.bind(this);
    }

    startCreatingPod() {
        // TODO update pod list to have a new PodItemCreator at the top of the list
        this.setState((state, props) => ({
            items: [...state.items, { id: 3 }]
        }));
    }

    render() {
        return (
            <div className="new-production-line">
                <div className="ui horizontal divider">
                    Pods
                </div>
                {this.state.items.map((item) =>
                    <PodItemCreator key={item.id} />
                )}
                <AddPodItem onAddNewPod={this.startCreatingPod} />
            </div>
        );
    }
}

