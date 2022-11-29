import React from 'react';

import PageHeader from "../components/PageHeader";
import { useApi } from "../hooks/use-api";

const Resources = () => {
    const opts: any = {
        
    };
    const {
        loading,
        data: resources
    } = useApi("/resources", opts);
    
    if (loading) {
        return <div>Loading...</div>;
    }
    
    return (
        <React.Fragment>
            <PageHeader text="Resources" />
            <pre>{JSON.stringify(resources, null, 2)}</pre>
        </React.Fragment>
    );
};

export default Resources;