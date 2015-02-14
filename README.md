Elastic Ops
==========
<img src="https://ci.appveyor.com/api/projects/status/uvu9ymptbd1lnfjw?svg=true" height="25px" />

ElasticOps is a desktop application which allows you to manage ElasticSearch cluster. 
It allows you to :
* [See basic cluster information ](#user-content-cluster)
  - Nodes
  - Indices
  - Document counts
  - Mappings
* [Query nodes ](#user-content-query)Query nodes (all endpoints are available) 
  - Editor contains intellisense for _search and _mapping endpoints (more are comming) 
  - Indices, types and endpoints are suggested 
* [Analyze](#user-content-analyze)Analyze - See how ElasticSearch sees your data
* [View your documents](#user-content-view-data), simply select index and type and you can browse docuemnts as sql table
* [Perform any REST request](#user-content-REST) (to ElasticSearch or any other API)
* [Change it's theme](#user-content-theme) just for fun :) 

Cluster
-------
![Cluster info](/docs/images/cluster_info.jpg)

Query
-------
Editor supports intellisense (real intellisense, it's aware in which part of the query is cursor and based on that suggests available options).

![Query - Intellisense](/docs/images/intellisense.jpg)

It also supports URL autocomplete. Index names, types and endpoints are suggested.

![Query - URL Autocomplete](/docs/images/url_suggest.jpg)

Analyze
-------
Analyze view allows you to send text to ElasticSearch and retrive list of tokens. You can analyze text using cluster or index analyzers. You can also make ElasticSearch to use the same analyzer as is used to analyzed given field.

Select a token on the list to highlight given token in original text.

![Analyze](/docs/images/analyze_and_highlight_tokens.jpg)

View data
---------
![View data](/docs/images/view_data.jpg)


REST
-------
![REST requests](/docs/images/REST_ES_enpoint_or_any_url.jpg)


Themes
-------
Just for fun you have control over application style. You can choose between light and dark theme and also among 23 different accents.

![Multiple themes](/docs/images/theme_dark_accents_list.jpg)

